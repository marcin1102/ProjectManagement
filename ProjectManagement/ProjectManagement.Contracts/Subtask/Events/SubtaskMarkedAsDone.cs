using System;
using Infrastructure.Message;
using ProjectManagement.Contracts.Issue.Enums;
using ProjectManagement.Contracts.Issue.Events;

namespace ProjectManagement.Contracts.Subtask.Events
{
    public class SubtaskMarkedAsDone : IIssueMarkedAsDone, IDomainEvent
    {
        public SubtaskMarkedAsDone(Guid id, IssueStatus status)
        {
            Id = id;
            Status = status;
        }

        public Guid Id { get; private set; }
        public IssueStatus Status { get; private set; }
        public long AggregateVersion { get; set; }
    }
}
