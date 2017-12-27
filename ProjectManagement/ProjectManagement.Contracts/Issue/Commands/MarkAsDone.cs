using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using ProjectManagement.Infrastructure.Primitives.Message;
using Newtonsoft.Json;

namespace ProjectManagement.Contracts.Issue.Commands
{
    // TODO: Change underlying contracts to interfaces
    public class MarkAsDone : ICommand
    {
        public MarkAsDone()
        {
        }

        [JsonIgnore]
        public Guid ProjectId { get; set; }
        [JsonIgnore]
        public Guid IssueId { get; set; }
    }

    public class MarkAsDoneValidator : AbstractValidator<MarkAsDone>
    {
        public MarkAsDoneValidator()
        {
            RuleFor(x => x.IssueId).NotNull();
        }
    }
}
