using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using Newtonsoft.Json;
using ProjectManagement.Contracts.Issue.Commands;

namespace ProjectManagement.Contracts.Nfr.Commands
{
    public class CreateNfr : ICreateAggregateIssue
    {
        public CreateNfr(string title, string description, Guid? assigneeId, ICollection<Guid> labelsIds)
        {
            Title = title;
            Description = description;
            AssigneeId = assigneeId;
            LabelsIds = labelsIds;
        }

        [JsonIgnore]
        public Guid ProjectId { get; set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
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

    public class CreateNfrValidator : AbstractValidator<CreateNfr>
    {
        public CreateNfrValidator()
        {
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
