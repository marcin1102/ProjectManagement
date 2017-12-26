using Newtonsoft.Json;
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
    public class GetIssue : IQuery<IssueResponse>
    {
        public GetIssue(Guid projectId, Guid issueId)
        {
            ProjectId = projectId;
            IssueId = issueId;
        }

        [JsonIgnore]
        public Guid ProjectId { get; }
        [JsonIgnore]
        public Guid IssueId { get; }
    }

    public class IssueResponse
    {
        public IssueResponse(Guid id, Guid projectId, Enums.IssueType issueType, string title, string description,
            IssueStatus status, Guid reporterId, string reporterFullName, string reporterEmail, Guid? assigneeId,
            IReadOnlyCollection<CommentResponse> comments, IReadOnlyCollection<LabelResponse> labels, long version)
        {
            Id = id;
            ProjectId = projectId;
            IssueType = issueType;
            Title = title;
            Description = description;
            Status = status;
            ReporterId = reporterId;
            ReporterFullName = reporterFullName;
            ReporterEmail = reporterEmail;
            AssigneeId = assigneeId;
            Comments = comments;
            Labels = labels;
            Version = version;
        }

        public Guid Id { get; }
        public Guid ProjectId { get; }
        public Enums.IssueType IssueType { get; }
        public string Title { get; }
        public string Description { get; }
        public IssueStatus Status { get; }
        public Guid ReporterId { get; }
        public string ReporterFullName { get; }
        public string ReporterEmail { get; }
        public Guid? AssigneeId { get; }
        public IReadOnlyCollection<CommentResponse> Comments { get; set; }
        public IReadOnlyCollection<LabelResponse> Labels { get; }
        public long Version { get; set; }

        public IReadOnlyCollection<LinkedIssue> LinkedIssues { get; private set; }


        public void SetLinkedIssues(IReadOnlyCollection<LinkedIssue> linkedIssues)
        {
            LinkedIssues = linkedIssues;
        }
    }

    public class LinkedIssue
    {
        public LinkedIssue(Guid id, string title, Enums.IssueType issueType)
        {
            Id = id;
            Title = title;
            IssueType = issueType;
        }

        public Guid Id { get; }
        public string Title { get; }
        public Enums.IssueType IssueType { get; }
    }

    public class CommentResponse
    {
        public CommentResponse(Guid memberId, string memberFullName, string content, DateTimeOffset addedAt)
        {
            MemberId = memberId;
            MemberFullName = memberFullName;
            Content = content;
            AddedAt = addedAt;
        }

        public Guid MemberId { get; }
        public string MemberFullName { get; }
        public string Content { get; }
        public DateTimeOffset AddedAt { get; }
    }

    public class LabelResponse
    {
        public LabelResponse(Guid id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }

        public Guid Id { get; set; }
        public string Name { get; }
        public string Description { get; }
    }
}
