using ProjectManagement.Infrastructure.Primitives.Message;
using ProjectManagement.Contracts.Sprint.Enums;
using ProjectManagement.Contracts.Sprint.ValueObjects;
using System;
using System.Collections.Generic;

namespace ProjectManagement.Contracts.Sprint.Events
{
    public class SprintFinished : IDomainEvent
    {
        public SprintFinished(Guid id, SprintStatus status, DateTime end, IReadOnlyCollection<Guid> unfinishedIssueIds)
        {
            Id = id;
            Status = status;
            End = end;
            UnfinishedIssueIds = unfinishedIssueIds;
        }

        public Guid Id { get; private set; }
        public SprintStatus Status { get; private set; }
        public DateTime End { get; private set; }
        public IReadOnlyCollection<Guid> UnfinishedIssueIds { get; private set; }
        public long AggregateVersion { get; set; }
    }
}
