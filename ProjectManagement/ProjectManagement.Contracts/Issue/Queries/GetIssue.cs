using System;
using System.Collections.Generic;
using ProjectManagement.Infrastructure.Primitives.Message;
using ProjectManagement.Contracts.Issue.Enums;

namespace ProjectManagement.Contracts.Issue.Queries
{
    public class GetIssue : IQuery<IssueResponse>
    {
        public Guid Id { get; set; }
    }

    public class IssueResponse
    {
        public IssueResponse(Guid id, Guid projectId, Guid? sprintId, string title, string description, IssueType type, IssueStatus status,
            Guid reporterId, Guid? assigneeId, DateTime createdAt, DateTime updatedAt,
            ICollection<Guid> subtasksIds, ICollection<Guid> labelsIds)
        {
            Id = id;
            ProjectId = projectId;
            SprintId = sprintId;
            Title = title;
            Description = description;
            Type = type;
            Status = status;
            ReporterId = reporterId;
            AssigneeId = assigneeId;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            //Comments = comments;
            SubtasksIds = subtasksIds;
            LabelsIds = labelsIds;
        }

        public Guid Id { get; private set; }
        public Guid ProjectId { get; private set; }
        public Guid? SprintId { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public IssueType Type { get; private set; }
        public IssueStatus Status { get; private set; }
        public Guid ReporterId { get; private set; }
        public Guid? AssigneeId { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }
        //public ICollection<Comment.Comment> Comments { get; private set; }
        public ICollection<Guid> SubtasksIds { get; private set; }
        public ICollection<Guid> LabelsIds { get; private set; }
    }
}
