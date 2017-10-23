using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManagement.Contracts.Issue.Commands;

namespace ProjectManagement.Contracts.Nfr.Commands
{
    public class AssignNfrToSprint : AssignIssueToSprint
    {
        public AssignNfrToSprint( Guid sprintId) : base(sprintId)
        {
        }
    }
}
