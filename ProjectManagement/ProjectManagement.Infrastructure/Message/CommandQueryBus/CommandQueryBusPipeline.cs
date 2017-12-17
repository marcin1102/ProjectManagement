using System;
using System.Threading.Tasks;
using Autofac;
using ProjectManagement.Infrastructure.Message.Handlers;
using ProjectManagement.Infrastructure.Message.Pipeline;
using ProjectManagement.Infrastructure.Primitives.Message;

namespace ProjectManagement.Infrastructure.Message.CommandQueryBus
{
    public class CommandQueryBusPipeline : ICommandQueryBus
    {
        private readonly IComponentContext container;

        public CommandQueryBusPipeline(IComponentContext container)
        {
            this.container = container;
        }

        public Task SendAsync(ICommand command)
        {
            var commandType = command.GetType();
            var handlerType = typeof(IAsyncCommandHandler<>).MakeGenericType(commandType);
            var wrapperType = typeof(AsyncCommandHandlerWrapper<>).MakeGenericType(commandType);

            var handler = container.Resolve(handlerType);
            var pipelineBuilder = container.Resolve<PipelineBuilder>();
            var wrapper = (AsyncCommandHandlerWrapper)Activator.CreateInstance(wrapperType, pipelineBuilder, handler);
            return wrapper.HandleAsync(command);
        }

        public Task<TResponse> SendAsync<TResponse>(IQuery<TResponse> query)
        {
            var responseType = typeof(TResponse);
            var queryType = query.GetType();
            var handlerType = typeof(IAsyncQueryHandler<,>).MakeGenericType(queryType, responseType);
            var wrapperType = typeof(AsyncQueryHandlerWrapper<,>).MakeGenericType(queryType, responseType);

            var handler = container.Resolve(handlerType);
            var pipelineBuilder = container.Resolve<PipelineBuilder>();
            var handlerWrapper = (AsyncQueryHandlerWrapper<TResponse>)Activator.CreateInstance(wrapperType, pipelineBuilder, handler);

            return handlerWrapper.HandleAsync(query);
        }


#region CommandWrapper
        private abstract class AsyncCommandHandlerWrapper
        {
            public abstract Task HandleAsync(ICommand command);
        }

        private class AsyncCommandHandlerWrapper<TCommand> : AsyncCommandHandlerWrapper
            where TCommand : ICommand
        {
            private readonly PipelineBuilder pipelineBuilder;
            private readonly IAsyncCommandHandler<TCommand> handler;

            public AsyncCommandHandlerWrapper(PipelineBuilder pipelineBuilder, IAsyncCommandHandler<TCommand> handler)
            {
                this.pipelineBuilder = pipelineBuilder;
                this.handler = handler;
            }

            public override Task HandleAsync(ICommand command)
            {
                var tCommand = (TCommand) command;
                var pipelineFirstItem = pipelineBuilder.BuildCommandPipeline(tCommand, handler);
                return pipelineFirstItem.HandleAsync(tCommand);
            }
        }
        #endregion

#region QueryWrapper
        public abstract class AsyncQueryHandlerWrapper<TResponse>
        {
            public abstract Task<TResponse> HandleAsync(IQuery<TResponse> query);
        }

        public class AsyncQueryHandlerWrapper<TQuery, TResponse> : AsyncQueryHandlerWrapper<TResponse>
            where TQuery : class, IQuery<TResponse>
            where TResponse : class
        {
            private readonly PipelineBuilder pipelineBuilder;
            private readonly IAsyncQueryHandler<TQuery, TResponse> handler;

            public AsyncQueryHandlerWrapper(PipelineBuilder pipelineBuilder, IAsyncQueryHandler<TQuery, TResponse> handler)
            {
                this.pipelineBuilder = pipelineBuilder;
                this.handler = handler;
            }

            public override Task<TResponse> HandleAsync(IQuery<TResponse> query)
            {
                var tQuery = (TQuery)query;
                return pipelineBuilder.BuildQueryPipeline(tQuery, handler).HandleAsync(tQuery);
            }
        }
        #endregion
    }
}
