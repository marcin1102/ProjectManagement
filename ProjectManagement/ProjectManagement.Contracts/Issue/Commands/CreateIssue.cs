using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentValidation;
using Infrastructure.Message;
using Newtonsoft.Json;
using ProjectManagement.Contracts.Issue.Enums;

namespace ProjectManagement.Contracts.Issue.Commands
{
    public class CreateIssue : ICommand
    {
        public CreateIssue(Guid projectId, string title, string description, IssueType type, Guid reporterId, Guid? assigneeId, ICollection<Guid> labelsIds, ICollection<Guid> subtasksIds)
        {
            ProjectId = projectId;
            Title = title;
            Description = description;
            Type = type;
            ReporterId = reporterId;
            AssigneeId = assigneeId;
            LabelsIds = labelsIds;
            SubtasksIds = subtasksIds;
        }

        public Guid ProjectId { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public IssueType Type { get; private set; }
        public Guid ReporterId { get; private set; }
        public Guid? AssigneeId { get; private set; }
        public ICollection<Guid> LabelsIds { get; private set; }
        public ICollection<Guid> SubtasksIds { get; private set; }

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

        public void SetSubtasks(ICollection<Guid> subtasksIds)
        {
            SubtasksIds = subtasksIds;
        }
    }

    public class CreateIssueValidator : AbstractValidator<CreateIssue>
    {
        public CreateIssueValidator()
        {
            RuleFor(x => x.ProjectId).NotNull();
            RuleFor(x => x.ReporterId).NotNull();
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.Type).NotNull();
            RuleFor(x => x.SubtasksIds).Must(NotNullInCollection);
            RuleFor(x => x.LabelsIds).Must(NotNullInCollection);
        }

        private bool NotNullInCollection(ICollection<Guid> collection)
        {
            return collection == null ? true : collection.All(x => x != null);
        }
    }
}
