using FluentValidation;
using Infrastructure.Message;
using System;

namespace ProjectManagement.Contracts.Sprint.Commands
{
    public class FinishSprint : ICommand
    {
        public FinishSprint(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }

    public class FinishSprintValidator : AbstractValidator<FinishSprint>
    {
        public FinishSprintValidator()
        {
            RuleFor(x => x.Id).NotNull();
        }
    }
}
