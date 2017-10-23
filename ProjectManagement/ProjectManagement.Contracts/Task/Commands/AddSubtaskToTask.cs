using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using Newtonsoft.Json;
using ProjectManagement.Contracts.Issue.Commands;

namespace ProjectManagement.Contracts.Task.Commands
{
    public class AddSubtaskToTask : IAddSubtaskTo
    {
        public AddSubtaskToTask(string title, string description, Guid reporterId, Guid? assigneeId, ICollection<Guid> labelsIds)
        {
            Title = title;
            Description = description;
            ReporterId = reporterId;
            AssigneeId = assigneeId;
            LabelsIds = labelsIds;
        }

        [JsonIgnore]
        public Guid TaskId { get; set; }
        [JsonIgnore]
        public Guid ProjectId { get; set; }
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

    public class AddSubtaskToTaskValidator : AbstractValidator<AddSubtaskToTask>
    {
        public AddSubtaskToTaskValidator()
        {
            RuleFor(x => x.ProjectId).NotNull();
            RuleFor(x => x.ReporterId).NotNull();
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.LabelsIds).Must(NotNullInCollection);
        }

        private object RuleFor(Func<object, object> p)
        {
            throw new NotImplementedException();
        }

        private bool NotNullInCollection(ICollection<Guid> collection)
        {
            return collection == null ? true : collection.All(x => x != null);
        }
    }
}
