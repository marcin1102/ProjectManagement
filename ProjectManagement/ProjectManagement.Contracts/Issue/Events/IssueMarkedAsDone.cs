using System;
using Infrastructure.Message;

namespace ProjectManagement.Contracts.Issue.Events
{
    public class IssueMarkedAsDone : IDomainEvent
    {
        public IssueMarkedAsDone(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
        public long AggregateVersion { get; set; }
    }
}
