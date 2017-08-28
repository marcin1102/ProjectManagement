using Autofac;
using Infrastructure.Message.Handlers;
using Infrastructure.Message.Pipeline.PipelineItems;
using Infrastructure.Message.Pipeline.PipelineItems.DefaultCommandPipelineItems;
using System.Linq;

namespace Infrastructure.Message.Pipeline
{
    public class PipelineBuilder
    {
        private readonly IComponentContext container;

        public PipelineBuilder(IComponentContext container)
        {
            this.container = container;
        }

        public CommandPipelineItem<TCommand> BuildPipeline<TCommand>(TCommand command, IAsyncCommandHandler<TCommand> handler)
            where TCommand : ICommand
        {
            var commandType = command.GetType();
            var pipelineItemsTypes = PredefinedCommandPipelines.TransactionCommandPipeline;

            var pipelineItems = pipelineItemsTypes
                .Select(x => x.MakeGenericType(commandType))
                .Select(x => container.Resolve(x))
                .Select(x => (CommandPipelineItem<TCommand>)x)
                .ToList();

            var lastItem = pipelineItems.Aggregate((current, next) => current.SetNextPipelineItem(next));

            lastItem.SetNextHandler(handler);

            return pipelineItems.First();
        }
    }
}
