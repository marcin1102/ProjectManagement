using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using Infrastructure.Message;

namespace ProjectManagement.Contracts.Issue.Commands
{
    public class MarkAsDone : ICommand
    {
        public MarkAsDone(Guid issueId, Guid userId)
        {
            IssueId = issueId;
            UserId = userId;
        }

        public Guid IssueId { get; private set; }
        public Guid UserId { get; private set; }
    }

    public class MarkAsDoneValidator : AbstractValidator<MarkAsDone>
    {
        public MarkAsDoneValidator()
        {
            RuleFor(x => x.IssueId).NotNull();
            RuleFor(x => x.UserId).NotNull();
        }
    }
}
