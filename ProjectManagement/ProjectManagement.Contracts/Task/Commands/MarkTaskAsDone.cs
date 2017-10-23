using System;
using ProjectManagement.Contracts.Issue.Commands;

namespace ProjectManagement.Contracts.Task.Commands
{
    public class MarkTaskAsDone : MarkAsDone
    {
        public MarkTaskAsDone(Guid userId) : base(userId)
        {
        }
    }
}
