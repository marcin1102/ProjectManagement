using FluentValidation;
using Infrastructure.Message;
using System;

namespace ProjectManagement.Contracts.Sprint.Commands
{
    public class StartSprint : ICommand
    {
        public StartSprint(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }

    public class StartSprintValidator : AbstractValidator<StartSprint>
    {
        public StartSprintValidator()
        {
            RuleFor(x => x.Id).NotNull();
        }
    }
}
