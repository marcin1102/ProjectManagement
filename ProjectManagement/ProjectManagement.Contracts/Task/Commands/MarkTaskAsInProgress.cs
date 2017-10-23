using System;
using ProjectManagement.Contracts.Issue.Commands;

namespace ProjectManagement.Contracts.Task.Commands
{
    public class MarkTaskAsInProgress : MarkAsInProgress
    {
        public MarkTaskAsInProgress(Guid userId) : base(userId)
        {
        }
    }
}
