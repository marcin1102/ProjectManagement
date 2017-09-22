using Infrastructure.Message;
using ProjectManagement.Contracts.Sprint.Enums;
using ProjectManagement.Contracts.Sprint.ValueObjects;
using System;
using System.Collections.Generic;

namespace ProjectManagement.Contracts.Sprint.Events
{
    public class SprintFinished : IDomainEvent
    {
        public SprintFinished(Guid id, SprintStatus status, ICollection<UnfinishedIssue> unfinishedIssues)
        {
            Id = id;
            Status = status;
            UnfinishedIssues = unfinishedIssues;
        }

        public Guid Id { get; private set; }
        public SprintStatus Status { get; private set; }
        public ICollection<UnfinishedIssue> UnfinishedIssues { get; private set; }
        public long AggregateVersion { get; set; }
    }
}
