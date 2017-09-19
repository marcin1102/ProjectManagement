using System;
using System.Collections.Generic;
using Infrastructure.Message;
using ProjectManagement.Contracts.Issue.Enums;

namespace ProjectManagement.Contracts.Issue.Queries
{
    public class GetIssue : IQuery<IssueResponse>
    {
        public Guid IssueId { get; set; }
    }

    public class IssueResponse
    {
        public IssueResponse(Guid id, Guid projectId, string title, string description, IssueType type, Status status, Guid reporterId, Guid? assigneeId, DateTime createdAt, DateTime updatedAt, string comments, ICollection<Guid> subtasksIds, ICollection<Guid> labelsIds)
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
            Comments = comments;
            SubtasksIds = subtasksIds;
            LabelsIds = labelsIds;
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
        public string Comments { get; private set; }
        public ICollection<Guid> SubtasksIds { get; private set; }
        public ICollection<Guid> LabelsIds { get; private set; }
    }
}
