using System.Threading.Tasks;

namespace Infrastructure.Message.Pipeline.PipelineItems.CommandPipelineItems
{
    public class FakeCommandPIpelineItem<TCommand> : CommandPipelineItem<TCommand>
        where TCommand : ICommand
    {
        public FakeCommandPIpelineItem()
        {
        }

        public override async Task HandleAsync(TCommand command)
        {
            await NextHandler.HandleAsync(command);
        }
    }
}
