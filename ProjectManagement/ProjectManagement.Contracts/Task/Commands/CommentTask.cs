using System;
using ProjectManagement.Contracts.Issue.Commands;

namespace ProjectManagement.Contracts.Task.Commands
{
    public class CommentTask : CommentIssue
    {
        public CommentTask(string content) : base(content)
        {
        }
    }
}
