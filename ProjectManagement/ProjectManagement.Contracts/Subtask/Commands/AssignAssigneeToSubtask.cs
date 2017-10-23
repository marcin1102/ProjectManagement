using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManagement.Contracts.Issue.Commands;

namespace ProjectManagement.Contracts.Subtask.Commands
{
    public class AssignAssigneeToSubtask : AssignAssigneeToIssue
    {
        public AssignAssigneeToSubtask(Guid subtaskId, Guid userId, Guid assigneeId) : base(subtaskId, userId, assigneeId)
        {
        }
    }
}
