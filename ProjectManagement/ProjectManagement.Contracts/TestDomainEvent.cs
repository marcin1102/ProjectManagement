using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Message;

namespace ProjectManagement.Contracts
{
    public class TestDomainEvent : IDomainEvent
    {
        public TestDomainEvent(Guid aggregateId)
        {
            Id = aggregateId;
        }

        public Guid Id { get; private set; }
        public long AggregateVersion { get; set; }
    }
}
