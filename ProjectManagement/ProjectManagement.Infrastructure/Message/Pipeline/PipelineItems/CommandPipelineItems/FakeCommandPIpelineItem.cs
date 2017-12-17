using ProjectManagement.Infrastructure.Primitives.Message;
using System.Threading.Tasks;

namespace ProjectManagement.Infrastructure.Message.Pipeline.PipelineItems.CommandPipelineItems
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
