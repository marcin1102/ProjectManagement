using Infrastructure.Message;
using ProjectManagement.Contracts.Sprint.Enums;
using System;

namespace ProjectManagement.Contracts.Sprint.Events
{
    public class SprintStarted : IDomainEvent
    {
        public SprintStarted(Guid id, SprintStatus status)
        {
            Id = id;
            Status = status;
        }

        public Guid Id { get; private set; }
        public SprintStatus Status { get; private set; }
        public long AggregateVersion { get; set; }
    }
}
