using System;
using ProjectManagement.Contracts.Issue.Commands;

namespace ProjectManagement.Contracts.Bug.Commands
{
    public class MarkBugAsInProgress : MarkAsInProgress
    {
        public MarkBugAsInProgress(Guid userId) : base(userId)
        {
        }
    }
}
