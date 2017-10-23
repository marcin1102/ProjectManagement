using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Message;

namespace ProjectManagement.Contracts.Issue.Events
{
    public interface IAssigneeAssigned
    {
        Guid IssueId { get; }
        Guid AssigneedId { get; }
    }
}
