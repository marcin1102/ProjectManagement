using Infrastructure.Message;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManagement.Contracts.Bug.Events
{
    public class ChildBugChangedToBug : IDomainEvent
    {
        public ChildBugChangedToBug(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }

        public long AggregateVersion { get; set; }
    }
}
