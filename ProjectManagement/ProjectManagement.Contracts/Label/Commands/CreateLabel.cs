using System;
using FluentValidation;
using Infrastructure.Message;
using Newtonsoft.Json;

namespace ProjectManagement.Contracts.Label.Commands
{
    public class CreateLabel : ICommand
    {
        public CreateLabel(Guid projectId, string name, string description)
        {
            ProjectId = projectId;
            Name = name;
            Description = description;
        }

        public Guid ProjectId { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }


        [JsonIgnore]
        public Guid CreatedId { get; set; }
    }

    public class CreateLabelValidator : AbstractValidator<CreateLabel>
    {
        public CreateLabelValidator()
        {
            RuleFor(x => x.ProjectId).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
        }
    }
}
