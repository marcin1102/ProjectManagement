using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Infrastructure.Message.Handlers;
using Infrastructure.Message.Pipeline;

namespace Infrastructure.Message.CommandQueryBus
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
            var wrapper = (AsyncCommandHandlerWrapper)Activator.CreateInstance(wrapperType, container, handler);
            return wrapper.HandleAsync(command);
        }

        public Task<TResponse> SendAsync<TResponse>(IQuery<TResponse> query)
        {
            throw new NotImplementedException();
        }

        private abstract class AsyncCommandHandlerWrapper
        {
            public abstract Task HandleAsync(ICommand command);
        }

        private class AsyncCommandHandlerWrapper<TCommand> : AsyncCommandHandlerWrapper
            where TCommand : ICommand
        {
            private readonly IComponentContext container;
            private readonly IAsyncCommandHandler<TCommand> handler;

            public AsyncCommandHandlerWrapper(IComponentContext container, IAsyncCommandHandler<TCommand> handler)
            {
                this.container = container;
                this.handler = handler;
            }

            public override Task HandleAsync(ICommand command)
            {
                var tCommand = (TCommand) command;
                var pipelineBuilder = container.Resolve<PipelineBuilder>();
                var pipelineFirstItem = pipelineBuilder.BuildPipeline(tCommand, handler);
                return pipelineFirstItem.HandleAsync(tCommand);
            }
        }

    }
}
