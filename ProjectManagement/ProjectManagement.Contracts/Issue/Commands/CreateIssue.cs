﻿using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Message;
using Newtonsoft.Json;
using ProjectManagement.Contracts.Issue.Enums;

namespace ProjectManagement.Contracts.Issue.Commands
{
    public class CreateIssue : ICommand
    {
        public CreateIssue(Guid projectId, string title, string description, IssueType type, Guid reporterId, Guid? assigneeId, ICollection<Guid> labelsIds)
        {
            ProjectId = projectId;
            Title = title;
            Description = description;
            Type = type;
            ReporterId = reporterId;
            AssigneeId = assigneeId;
            LabelsIds = labelsIds;
        }

        public Guid ProjectId { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public IssueType Type { get; private set; }
        public Guid ReporterId { get; private set; }
        public Guid? AssigneeId { get; private set; }
        public ICollection<Guid> LabelsIds { get; private set; }

        [JsonIgnore]
        public Guid CreatedId { get; set; }

        public void SetAssignee(Guid assigneeId)
        {
            AssigneeId = assigneeId;
        }

        public void SetLabels(ICollection<Guid> labelsIds)
        {
            LabelsIds = labelsIds;
        }
    }
}
