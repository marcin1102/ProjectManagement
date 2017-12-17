using Autofac;
using ProjectManagement.Infrastructure.Message.Handlers;
using ProjectManagement.Infrastructure.Message.Pipeline.PipelineItems;
using ProjectManagement.Infrastructure.Message.Pipeline.PipelineItems.CommandPipelineItems;
using System.Linq;
using ProjectManagement.Infrastructure.Message.Pipeline.PipelineItems.QueryPipelineItems;
using ProjectManagement.Infrastructure.Primitives.Message;

namespace ProjectManagement.Infrastructure.Message.Pipeline
{

    public class PipelineBuilder
    {
        private readonly IComponentContext container;
        private readonly IPipelineItemsConfiguration pipelineConfiguration;

        public PipelineBuilder(IComponentContext container, IPipelineItemsConfiguration pipelineConfiguration)
        {
            this.container = container;
            this.pipelineConfiguration = pipelineConfiguration;
        }

        public CommandPipelineItem<TCommand> BuildCommandPipeline<TCommand>(TCommand command, IAsyncCommandHandler<TCommand> handler)
            where TCommand : ICommand
        {
            var commandType = command.GetType();
            var pipelineItemsTypes = pipelineConfiguration.GetCommandPipelineItems<TCommand>();

            var pipelineItems = pipelineItemsTypes
                .Select(x => x.MakeGenericType(commandType))
                .Select(x => container.Resolve(x))
                .Select(x => (CommandPipelineItem<TCommand>)x)
                .ToList();

            var lastItem = pipelineItems.Aggregate((current, next) => current.SetNextPipelineItem(next));

            lastItem.SetNextHandler(handler);

            return pipelineItems.First();
        }

        public QueryPipelineItem<TQuery, TResponse> BuildQueryPipeline<TQuery, TResponse>(TQuery query, IAsyncQueryHandler<TQuery, TResponse> handler)
            where TQuery : IQuery<TResponse>
        {
            var queryType = query.GetType();
            var responseType = typeof(TResponse);
            var pipelineItemsTypes = pipelineConfiguration.GetQueryPIpelineITems<TQuery, TResponse>();

            var pipelineItems = pipelineItemsTypes
                .Select(x => x.MakeGenericType(queryType, responseType))
                .Select(x => container.Resolve(x))
                .Select(x => (QueryPipelineItem<TQuery, TResponse>)x)
                .ToList();

            var lastItem = pipelineItems.Aggregate((current, next) => current.SetNextPipelineItem(next));
            lastItem.SetNextHandler(handler);
            return pipelineItems.First();
        }

    }
}
