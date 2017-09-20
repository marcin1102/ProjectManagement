using System;
using FluentValidation;
using Infrastructure.Message;

namespace ProjectManagement.Contracts.Issue.Commands
{
    public class AddSubtask : ICommand
    {
        public AddSubtask(Guid issueId, Guid subtaskId)
        {
            IssueId = issueId;
            SubtaskId = subtaskId;
        }

        public Guid IssueId { get; private set; }
        public Guid SubtaskId { get; private set; }
    }

    public class AddSubtaskValidator : AbstractValidator<AddSubtask>
    {
        public AddSubtaskValidator()
        {
            RuleFor(x => x.SubtaskId).NotNull();
            RuleFor(x => x.IssueId).NotNull();
        }
    }
}
