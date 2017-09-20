using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using Infrastructure.Message;

namespace ProjectManagement.Contracts.Issue.Commands
{
    public class AssignLabelsToIssue : ICommand
    {
        public AssignLabelsToIssue(Guid issueId, ICollection<Guid> labelsIds)
        {
            IssueId = issueId;
            LabelsIds = labelsIds;
        }

        public Guid IssueId { get; private set; }
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
