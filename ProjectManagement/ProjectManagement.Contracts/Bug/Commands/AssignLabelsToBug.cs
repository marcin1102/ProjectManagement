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
        public AssignLabelsToBug(Guid bugId, ICollection<Guid> labelsIds) : base(bugId, labelsIds)
        {
        }

        public Guid? TaskId { get; private set; }
        public Guid? NfrId { get; private set; }
    }
}
