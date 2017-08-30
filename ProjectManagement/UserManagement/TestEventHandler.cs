using System.Threading.Tasks;
using Infrastructure.Message.Handlers;
using ProjectManagement.Contracts;

namespace UserManagement
{
    public class TestEventHandler : IAsyncEventHandler<TestDomainEvent>
    {
        public Task HandleAsync(TestDomainEvent @event)
        {
            return Task.CompletedTask;
        }
    }
}
