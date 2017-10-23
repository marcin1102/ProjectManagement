using FluentValidation;
using Infrastructure.Message;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManagement.Contracts.Sprint.Commands
{
    public class CreateSprint : ICommand
    {
        public CreateSprint(string name, DateTime start, DateTime end)
        {
            Name = name;
            Start = start;
            End = end;
        }

        [JsonIgnore]
        public Guid ProjectId { get; set; }
        public string Name { get; private set; }
        public DateTime Start { get; private set; }
        public DateTime End { get; private set; }

        [JsonIgnore]
        public Guid CreatedId { get; set; }
    }

    public class CreateSprintValidator : AbstractValidator<CreateSprint>
    {
        public CreateSprintValidator()
        {
            RuleFor(x => x.ProjectId).NotNull();
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Start).NotNull();
            RuleFor(x => x.End).NotNull();
        }
    }
}
