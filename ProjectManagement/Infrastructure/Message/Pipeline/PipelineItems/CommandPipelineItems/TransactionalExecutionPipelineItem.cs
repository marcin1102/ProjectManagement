using System.Threading.Tasks;

namespace Infrastructure.Message.Pipeline.PipelineItems.CommandPipelineItems
{
    public class TransactionalExecutionPipelineItem<TCommand> : CommandPipelineItem<TCommand>
        where TCommand : ICommand
    {
        //Configure database connection first

        public TransactionalExecutionPipelineItem()
        {
        }

        public override Task HandleAsync(TCommand command)
        {
            return base.HandleAsync(command);
        }
    }
}
