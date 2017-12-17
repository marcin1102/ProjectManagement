using System;
using System.Collections.Generic;
using System.Text;
using ProjectManagement.Infrastructure.Primitives.Message;

namespace ProjectManagement.Contracts.Issue.Events
{
    public interface IAssigneeAssigned
    {
        Guid IssueId { get; }
        Guid AssigneeId { get; }
    }
}
