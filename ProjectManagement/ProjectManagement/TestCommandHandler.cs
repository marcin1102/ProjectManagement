using System;
using System.Threading.Tasks;
using Infrastructure.Message.EventDispatcher;
using Infrastructure.Message.Handlers;
using ProjectManagement.Contracts;

namespace ProjectManagement
{
    public class TestCommandHandler :
        IAsyncCommandHandler<TestCommand>
    {
        private readonly ProjectManagementContext context;
        private readonly IDomainEventDispatcher eventDispatcher;
        public TestCommandHandler(ProjectManagementContext context, IDomainEventDispatcher eventDispatcher)
        {
            this.context = context;
            this.eventDispatcher = eventDispatcher;
        }

        public Task HandleAsync(TestCommand command)
        {
            var i = command.TestValue;

            return eventDispatcher.Dispatch(new TestDomainEvent(Guid.NewGuid(), 0));
        }
    }
}
