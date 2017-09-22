using Infrastructure.Message;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManagement.Contracts.Sprint.Events
{
    public class SprintCreated : IDomainEvent
    {
        public SprintCreated(Guid id, string name, DateTime start, DateTime end)
        {
            Id = id;
            Name = name;
            Start = start;
            End = end;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public DateTime Start { get; private set; }
        public DateTime End { get; private set; }
        public long AggregateVersion { get; set; }
    }
}
