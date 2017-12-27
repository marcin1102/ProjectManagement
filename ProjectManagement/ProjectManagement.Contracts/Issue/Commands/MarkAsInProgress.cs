using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using ProjectManagement.Infrastructure.Primitives.Message;
using Newtonsoft.Json;

namespace ProjectManagement.Contracts.Issue.Commands
{
    public class MarkAsInProgress : ICommand
    {
        public MarkAsInProgress()
        {
        }

        [JsonIgnore]
        public Guid ProjectId { get; set; }
        [JsonIgnore]
        public Guid IssueId { get; set; }
    }

    public class MarkAsInProgressValidator : AbstractValidator<MarkAsInProgress>
    {
        public MarkAsInProgressValidator()
        {
            RuleFor(x => x.IssueId).NotNull();
        }
    }
}
