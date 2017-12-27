using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManagement.Contracts.Issue.Commands;

namespace ProjectManagement.Contracts.Task.Commands
{
    public class AssignAssigneeToTask : AssignAssigneeToIssue
    {
        public AssignAssigneeToTask(Guid assigneeId) : base(assigneeId)
        {
        }
    }
}
