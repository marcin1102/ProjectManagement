using ProjectManagementView.Contracts.Issues.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementView.Contracts.Issues
{
    public class UnfinishedIssue
    {
        private UnfinishedIssue()
        {

        }

        public UnfinishedIssue(Guid issueId, IssueType issueType, Guid? assigneeId)
        {
            IssueId = issueId;
            IssueType = issueType;
            AssigneeId = assigneeId;
        }

        public Guid IssueId { get; private set; }
        public IssueType IssueType { get; private set; }
        public Guid? AssigneeId { get; private set; }
    }
}
