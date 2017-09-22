using System;
using Infrastructure.Message;
using ProjectManagement.Contracts.Issue.Enums;

namespace ProjectManagement.Contracts.Issue.Events
{
    public class IssueMarkedAsInProgress : IDomainEvent
    {
        public IssueMarkedAsInProgress(Guid id, IssueStatus status)
        {
            Id = id;
            Status = status;
        }

        public Guid Id { get; private set; }
        public IssueStatus Status { get; private set; }
        public long AggregateVersion { get; set; }
    }
}
