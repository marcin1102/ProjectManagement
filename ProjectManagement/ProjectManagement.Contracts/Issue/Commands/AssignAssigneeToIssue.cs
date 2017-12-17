using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using ProjectManagement.Infrastructure.Primitives.Message;
using Newtonsoft.Json;

namespace ProjectManagement.Contracts.Issue.Commands
{
    public class AssignAssigneeToIssue : ICommand
    {
        public AssignAssigneeToIssue(Guid userId, Guid assigneeId)
        {
            UserId = userId;
            AssigneeId = assigneeId;
        }

        [JsonIgnore]
        public Guid ProjectId { get; set; }
        [JsonIgnore]
        public Guid IssueId { get; set; }
        public Guid UserId { get; private set; }
        public Guid AssigneeId { get; private set; }
    }

    public class AssignAssigneeToIssueValidator : AbstractValidator<AssignAssigneeToIssue>
    {
        public AssignAssigneeToIssueValidator()
        {
            RuleFor(x => x.AssigneeId).NotNull();
            RuleFor(x => x.IssueId).NotNull();
        }
    }
}
