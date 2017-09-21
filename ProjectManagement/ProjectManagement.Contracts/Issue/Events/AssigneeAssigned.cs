using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Message;

namespace ProjectManagement.Contracts.Issue.Events
{
    public class AssigneeAssigned : IDomainEvent
    {
        public AssigneeAssigned(Guid issueId, Guid assigneedId)
        {
            IssueId = issueId;
            AssigneedId = assigneedId;
        }

        public Guid IssueId { get; private set; }
        public Guid AssigneedId { get; private set; }
        public long AggregateVersion { get; set; }
    }
}
