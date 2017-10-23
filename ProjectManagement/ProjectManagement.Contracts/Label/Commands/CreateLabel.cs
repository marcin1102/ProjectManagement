using System;
using FluentValidation;
using Infrastructure.Message;
using Newtonsoft.Json;

namespace ProjectManagement.Contracts.Label.Commands
{
    public class CreateLabel : ICommand
    {
        public CreateLabel(string name, string description)
        {
            Name = name;
            Description = description;
        }

        [JsonIgnore]
        public Guid ProjectId { get; set; }
        public string Name { get; private set; }
        public string Description { get; private set; }


        [JsonIgnore]
        public Guid CreatedId { get; set; }
    }

    public class CreateLabelValidator : AbstractValidator<CreateLabel>
    {
        public CreateLabelValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
        }
    }
}
