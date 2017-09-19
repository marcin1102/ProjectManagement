using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Message;
using Newtonsoft.Json;
using ProjectManagement.Contracts.Issue.Enums;

namespace ProjectManagement.Contracts.Issue.Commands
{
    public class CreateIssue : ICommand
    {
        public CreateIssue(Guid projectId, string title, string description, IssueType type, Status status, Guid reporterId, Guid? assigneeId, IReadOnlyCollection<Guid> labelsIds)
        {
            ProjectId = projectId;
            Title = title;
            Description = description;
            Type = type;
            Status = status;
            ReporterId = reporterId;
            AssigneeId = assigneeId;
            LabelsIds = labelsIds;
        }

        public Guid ProjectId { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public IssueType Type { get; private set; }
        public Status Status { get; private set; }
        public Guid ReporterId { get; private set; }
        public Guid? AssigneeId { get; private set; }
        public IReadOnlyCollection<Guid> LabelsIds { get; private set; }

        [JsonIgnore]
        public Guid CreatedId { get; set; }
    }
}
