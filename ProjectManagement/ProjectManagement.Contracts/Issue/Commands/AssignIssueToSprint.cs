using FluentValidation;
using ProjectManagement.Infrastructure.Primitives.Message;
using System;
using Newtonsoft.Json;

namespace ProjectManagement.Contracts.Issue.Commands
{
    public class AssignIssueToSprint : ICommand
    {
        public AssignIssueToSprint(Guid sprintId)
        {
            SprintId = sprintId;
        }

        [JsonIgnore]
        public Guid ProjectId { get; set; }
        [JsonIgnore]
        public Guid IssueId { get; set; }
        public Guid SprintId { get; private set; }
    }

    public class AssignIssueToSprintValidator : AbstractValidator<AssignIssueToSprint>
    {
        public AssignIssueToSprintValidator()
        {
            RuleFor(x => x.IssueId).NotNull();
            RuleFor(x => x.SprintId).NotNull();
        }
    }
}
