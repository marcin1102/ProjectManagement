using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManagement.Contracts.Sprint.ValueObjects
{
    public class UnfinishedIssue
    {
        public UnfinishedIssue(Guid issueId, Guid? userId)
        {
            IssueId = issueId;
            UserId = userId;
        }

        public Guid IssueId { get; }
        public Guid? UserId { get; }
    }
}
