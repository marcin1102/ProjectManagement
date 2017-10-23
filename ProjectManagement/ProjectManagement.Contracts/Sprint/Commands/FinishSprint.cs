using FluentValidation;
using Infrastructure.Message;
using System;

namespace ProjectManagement.Contracts.Sprint.Commands
{
    public class FinishSprint : ICommand
    {
        public FinishSprint(Guid id, Guid projectId)
        {
            Id = id;
            ProjectId = projectId;
        }

        public Guid Id { get; private set; }
        public Guid ProjectId { get; private set; }
    }

    public class FinishSprintValidator : AbstractValidator<FinishSprint>
    {
        public FinishSprintValidator()
        {
            RuleFor(x => x.Id).NotNull();
            RuleFor(x => x.ProjectId).NotNull();
        }
    }
}
