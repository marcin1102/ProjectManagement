using Infrastructure.Message;
using ProjectManagement.Contracts.Sprint.Enums;
using ProjectManagement.Contracts.Sprint.ValueObjects;
using System;
using System.Collections.Generic;

namespace ProjectManagement.Contracts.Sprint.Events
{
    public class SprintFinished : IDomainEvent
    {
        public SprintFinished(Guid id, SprintStatus status, DateTime end)
        {
            Id = id;
            Status = status;
            End = end;
        }

        public Guid Id { get; private set; }
        public SprintStatus Status { get; private set; }
        public DateTime End { get; set; }
        public long AggregateVersion { get; set; }
    }
}
