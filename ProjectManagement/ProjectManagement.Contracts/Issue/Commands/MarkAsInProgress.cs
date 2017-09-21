using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using Infrastructure.Message;

namespace ProjectManagement.Contracts.Issue.Commands
{
    public class MarkAsInProgress : ICommand
    {
        public MarkAsInProgress(Guid issueId, Guid userId)
        {
            IssueId = issueId;
            UserId = userId;
        }

        public Guid IssueId { get; private set; }
        public Guid UserId { get; private set; }
    }

    public class MarkAsInProgressValidator : AbstractValidator<MarkAsInProgress>
    {
        public MarkAsInProgressValidator()
        {
            RuleFor(x => x.IssueId).NotNull();
            RuleFor(x => x.UserId).NotNull();
        }
    }
}
