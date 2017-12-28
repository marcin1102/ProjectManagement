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
        private UnfinishedIssue() { }
        public UnfinishedIssue(Guid issueId, IssueType issueType, string title)
        {
            IssueId = issueId;
            IssueType = issueType;
            Title = title;
        }

        public Guid IssueId { get; private set; }
        public IssueType IssueType { get; }
        public string Title { get; }
        public string TitleWithType { get => $"{IssueType.ToString()}: {Title}"; }
    }
}
