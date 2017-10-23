using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManagement.Contracts.Issue.Commands;

namespace ProjectManagement.Contracts.Subtask.Commands
{
    public class AssignSubtaskToSprint : AssignIssueToSprint
    {
        public AssignSubtaskToSprint(Guid subtaskId, Guid sprintId) : base(subtaskId, sprintId)
        {
        }
    }
}
