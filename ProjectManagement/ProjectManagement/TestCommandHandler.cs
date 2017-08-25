using System.Threading.Tasks;
using Infrastructure.Message.Handlers;
using ProjectManagement.Contracts;

namespace ProjectManagement
{
    public class TestCommandHandler
        : IAsyncCommandHandler<TestCommand>
    {
        public TestCommandHandler()
        {

        }

        public Task HandleAsync(TestCommand command)
        {
            var i = command.TestValue;
            return Task.CompletedTask;
        }
    }
}
