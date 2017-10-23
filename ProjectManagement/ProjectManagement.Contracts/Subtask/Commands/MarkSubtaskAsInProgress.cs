using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManagement.Contracts.Issue.Commands;

namespace ProjectManagement.Contracts.Subtask.Commands
{
    public class MarkSubtaskAsInProgress : MarkAsInProgress
    {
        public MarkSubtaskAsInProgress(Guid bugId, Guid userId) : base(bugId, userId)
        {
        }
    }
}
