using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using ProjectManagement.Infrastructure.Primitives.Message;
using Newtonsoft.Json;

namespace ProjectManagement.Contracts.Issue.Commands
{
    public class AssignLabelsToIssue : ICommand
    {
        public AssignLabelsToIssue(ICollection<Guid> labelsIds)
        {
            LabelsIds = labelsIds;
        }

        [JsonIgnore]
        public Guid ProjectId { get; set; }
        [JsonIgnore]
        public Guid IssueId { get; set; }
        public ICollection<Guid> LabelsIds { get; private set; }
    }

    public class AssignLabelsToIssueValidator : AbstractValidator<AssignLabelsToIssue>
    {
        public AssignLabelsToIssueValidator()
        {
            RuleFor(x => x.IssueId).NotNull();
            RuleFor(x => x.LabelsIds).NotNull();
        }
    }
}
