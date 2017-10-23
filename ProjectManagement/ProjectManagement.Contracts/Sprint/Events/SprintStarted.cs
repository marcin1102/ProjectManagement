using Infrastructure.Message;
using ProjectManagement.Contracts.Sprint.Enums;
using System;

namespace ProjectManagement.Contracts.Sprint.Events
{
    public class SprintStarted : IDomainEvent
    {
        public SprintStarted(Guid id, SprintStatus status, DateTime start)
        {
            Id = id;
            Status = status;
            Start = start;
        }

        public Guid Id { get; private set; }
        public SprintStatus Status { get; private set; }
        public DateTime Start { get; set; }
        public long AggregateVersion { get; set; }
    }
}
