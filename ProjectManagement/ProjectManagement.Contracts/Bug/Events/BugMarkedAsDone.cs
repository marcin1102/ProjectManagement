using System;
using ProjectManagement.Infrastructure.Primitives.Message;
using ProjectManagement.Contracts.Issue.Enums;
using ProjectManagement.Contracts.Issue.Events;

namespace ProjectManagement.Contracts.Bug.Events
{
    public class BugMarkedAsDone : IIssueMarkedAsDone, IDomainEvent
    {
        public BugMarkedAsDone(Guid id, IssueStatus status)
        {
            Id = id;
            Status = status;
        }

        public Guid Id { get; private set; }
        public IssueStatus Status { get; private set; }
        public long AggregateVersion { get; set; }
    }
}
