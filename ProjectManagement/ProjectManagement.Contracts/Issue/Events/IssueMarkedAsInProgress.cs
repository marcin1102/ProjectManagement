using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Message;

namespace ProjectManagement.Contracts.Issue.Events
{
    public class IssueMarkedAsInProgress : IDomainEvent
    {
        public IssueMarkedAsInProgress(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
        public long AggregateVersion { get; set; }
    }
}
