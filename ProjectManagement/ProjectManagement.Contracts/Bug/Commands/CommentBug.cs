using System;
using ProjectManagement.Contracts.Issue.Commands;

namespace ProjectManagement.Contracts.Bug.Commands
{
    public class CommentBug : CommentIssue
    {
        public CommentBug(string content) : base(content)
        {
        }
    }
}
