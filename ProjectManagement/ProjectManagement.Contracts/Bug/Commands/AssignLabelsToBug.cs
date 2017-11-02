using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManagement.Contracts.Issue.Commands;

namespace ProjectManagement.Contracts.Bug.Commands
{
    public class AssignLabelsToBug : AssignLabelsToIssue
    {
        public AssignLabelsToBug(ICollection<Guid> labelsIds) : base(labelsIds)
        {
        }
    }
}
