using Infrastructure.Message;
using System;
using System.Collections.Generic;
using System.Text;
using ProjectManagement.Contracts.Sprint.Enums;

namespace ProjectManagement.Contracts.Sprint.Events
{
    public class SprintCreated : IDomainEvent
    {
        public SprintCreated(Guid id, Guid projectId, string name, DateTime start, DateTime end, SprintStatus status)
        {
            Id = id;
            ProjectId = projectId;
            Name = name;
            Start = start;
            End = end;
            Status = status;
        }

        public Guid Id { get; private set; }
        public Guid ProjectId { get; private set; }
        public string Name { get; private set; }
        public DateTime Start { get; private set; }
        public DateTime End { get; private set; }
        public SprintStatus Status { get; private set; }
        public long AggregateVersion { get; set; }
    }
}
