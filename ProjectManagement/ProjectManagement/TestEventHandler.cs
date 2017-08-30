using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Message.Handlers;
using ProjectManagement.Contracts;

namespace ProjectManagement
{
    public class TestEventHandler :
        IAsyncEventHandler<TestDomainEvent>
    {
        public Task HandleAsync(TestDomainEvent @event)
        {
            return Task.CompletedTask;
        }
    }
}
