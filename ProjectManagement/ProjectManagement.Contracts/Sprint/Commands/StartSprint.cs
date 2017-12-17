using FluentValidation;
using ProjectManagement.Infrastructure.Primitives.Message;
using System;
using Newtonsoft.Json;

namespace ProjectManagement.Contracts.Sprint.Commands
{
    public class StartSprint : ICommand
    {
        public StartSprint(Guid id, Guid projectId)
        {
            Id = id;
            ProjectId = projectId;
        }

        public Guid Id { get; private set; }
        public Guid ProjectId { get; private set; }
    }

    public class StartSprintValidator : AbstractValidator<StartSprint>
    {
        public StartSprintValidator()
        {
            RuleFor(x => x.Id).NotNull();
            RuleFor(x => x.ProjectId).NotNull();
        }
    }
}
