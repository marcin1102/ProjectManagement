using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManagement.Contracts.Issue.Commands;

namespace ProjectManagement.Contracts.Subtask.Commands
{
    public class AssignLabelsToSubtask : AssignLabelsToIssue
    {
        public AssignLabelsToSubtask(Guid subtaskId, ICollection<Guid> labelsIds) : base(subtaskId, labelsIds)
        {
        }

        public Guid TaskId { get; private set; }
    }
}
