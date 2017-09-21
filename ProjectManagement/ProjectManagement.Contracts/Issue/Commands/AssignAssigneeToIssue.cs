using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using Infrastructure.Message;

namespace ProjectManagement.Contracts.Issue.Commands
{
    public class AssignAssigneeToIssue : ICommand
    {
        public AssignAssigneeToIssue(Guid issueId, Guid userId, Guid assigneeId)
        {
            IssueId = issueId;
            UserId = userId;
            AssigneeId = assigneeId;
        }

        public Guid IssueId { get; private set; }
        public Guid UserId { get; private set; }
        public Guid AssigneeId { get; private set; }
    }

    public class AssignAssigneeToIssueValidator : AbstractValidator<AssignAssigneeToIssue>
    {
        public AssignAssigneeToIssueValidator()
        {
            RuleFor(x => x.AssigneeId).NotNull();
            RuleFor(x => x.IssueId).NotNull();
            RuleFor(x => x.UserId).NotNull();
        }
    }
}
