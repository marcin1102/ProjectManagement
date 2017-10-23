using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using Infrastructure.Message;
using Newtonsoft.Json;

namespace ProjectManagement.Contracts.Issue.Commands
{
    public class MarkAsInProgress : ICommand
    {
        public MarkAsInProgress(Guid userId)
        {
            UserId = userId;
        }

        [JsonIgnore]
        public Guid ProjectId { get; set; }
        [JsonIgnore]
        public Guid IssueId { get; set; }
        public Guid UserId { get; private set; }
    }

    public class MarkAsInProgressValidator : AbstractValidator<MarkAsInProgress>
    {
        public MarkAsInProgressValidator()
        {
            RuleFor(x => x.IssueId).NotNull();
            RuleFor(x => x.UserId).NotNull();
        }
    }
}
