using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using Newtonsoft.Json;
using ProjectManagement.Contracts.Issue.Commands;

namespace ProjectManagement.Contracts.Subtask.Commands
{
    public class CreateSubtask : ICreateIssue
    {
        public CreateSubtask(Guid projectId, string title, string description, Guid reporterId, Guid? assigneeId, ICollection<Guid> labelsIds)
        {
            ProjectId = projectId;
            Title = title;
            Description = description;
            ReporterId = reporterId;
            AssigneeId = assigneeId;
            LabelsIds = labelsIds;
        }

        public Guid ProjectId { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
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

    public class CreateSubtaskValidator : AbstractValidator<CreateSubtask>
    {
        public CreateSubtaskValidator()
        {
            RuleFor(x => x.ProjectId).NotNull();
            RuleFor(x => x.ReporterId).NotNull();
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.LabelsIds).Must(NotNullInCollection);
        }

        private bool NotNullInCollection(ICollection<Guid> collection)
        {
            return collection == null ? true : collection.All(x => x != null);
        }
    }
}
