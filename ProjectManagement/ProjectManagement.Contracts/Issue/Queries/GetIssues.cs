using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Message;
using ProjectManagement.Contracts.Issue.Enums;

namespace ProjectManagement.Contracts.Issue.Queries
{
    public class GetIssues : IQuery<ICollection<IssueListItem>>
    {
        public Guid ProjectId { get; set; }
        public Guid? ReporterId { get; set; }
        public Guid? AssigneeId { get; set; }
    }

    public class IssueListItem
    {
        public IssueListItem(Guid id, Guid projectId, string title, string description, IssueType type, Status status, Guid reporterId, Guid? assigneeId, DateTime createdAt, DateTime updatedAt)
        {
            Id = id;
            ProjectId = projectId;
            Title = title;
            Description = description;
            Type = type;
            Status = status;
            ReporterId = reporterId;
            AssigneeId = assigneeId;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }

        public Guid Id { get; private set; }
        public Guid ProjectId { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public IssueType Type { get; private set; }
        public Status Status { get; private set; }
        public Guid ReporterId { get; private set; }
        public Guid? AssigneeId { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }
    }
}
