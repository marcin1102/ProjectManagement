using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Message;

namespace ProjectManagement.Contracts.Issue.Events
{
    public class SubtaskAdded : IDomainEvent
    {
        public SubtaskAdded(Guid issueId, Guid subtaskId)
        {
            IssueId = issueId;
            SubtaskId = subtaskId;
        }

        public Guid IssueId { get; private set; }
        public Guid SubtaskId { get; private set; }
        public long AggregateVersion { get; set; }
    }
}
