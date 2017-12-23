using ProjectManagement.Contracts.Issue.Enums;
using ProjectManagement.Infrastructure.Primitives.Message;
using ProjectManagementView.Contracts.Issues.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementView.Contracts.Issues
{
    public class GetIssues : IQuery<IReadOnlyCollection<IssueListItem>>
    {
        public GetIssues(Guid projectId)
        {
            ProjectId = projectId;
        }

        public Guid ProjectId { get; private set; }
    }

    public class IssueListItem
    {
        public IssueListItem(Guid id, Guid projectId, Enums.IssueType issueType, string title, string description, IssueStatus status, Guid reporterId, Guid? assigneeId)
        {
            Id = id;
            ProjectId = projectId;
            IssueType = issueType;
            Title = title;
            Description = description;
            Status = status;
            ReporterId = reporterId;
            AssigneeId = assigneeId;
        }

        public Guid Id { get; }
        public Guid ProjectId { get; }
        public Enums.IssueType IssueType { get; }
        public string Title { get; }
        public string Description { get; }
        public IssueStatus Status { get; }
        public Guid ReporterId { get; }
        public Guid? AssigneeId { get; }
    }
}
