using FluentValidation;
using Infrastructure.Message;
using System;

namespace ProjectManagement.Contracts.Issue.Commands
{
    public class AssignIssueToSprint : ICommand
    {
        public AssignIssueToSprint(Guid issueId, Guid sprintId)
        {
            IssueId = issueId;
            SprintId = sprintId;
        }

        public Guid IssueId { get; private set; }
        public Guid SprintId { get; private set; }
    }

    public class AssignIssueToSprintValidator : AbstractValidator<AssignIssueToSprint>
    {
        public AssignIssueToSprintValidator()
        {
            RuleFor(x => x.IssueId).NotNull();
            RuleFor(x => x.SprintId).NotNull();
        }
    }
}
