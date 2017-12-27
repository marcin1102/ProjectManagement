using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManagement.Contracts.Issue.Commands;

namespace ProjectManagement.Contracts.Nfr.Commands
{
    public class AssignAssigneeToNfr : AssignAssigneeToIssue
    {
        public AssignAssigneeToNfr(Guid assigneeId) : base(assigneeId)
        {
        }
    }
}
