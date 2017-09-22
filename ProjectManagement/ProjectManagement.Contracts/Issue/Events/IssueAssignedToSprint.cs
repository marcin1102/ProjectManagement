using Infrastructure.Message;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManagement.Contracts.Issue.Events
{
    public class IssueAssignedToSprint : IDomainEvent
    {
        public IssueAssignedToSprint(Guid issueId, Guid sprintId)
        {
            IssueId = issueId;
            SprintId = sprintId;
        }

        public Guid IssueId { get; private set; }
        public Guid SprintId { get; private set; }
        public long AggregateVersion { get; set; }
    }
}
