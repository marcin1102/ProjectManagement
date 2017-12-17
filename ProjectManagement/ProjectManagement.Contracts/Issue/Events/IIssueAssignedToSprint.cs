using ProjectManagement.Infrastructure.Primitives.Message;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManagement.Contracts.Issue.Events
{
    public interface IIssueAssignedToSprint
    {
        Guid IssueId { get; }
        Guid SprintId { get; }
    }
}
