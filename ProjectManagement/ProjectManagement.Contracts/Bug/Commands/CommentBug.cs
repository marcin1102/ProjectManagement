using System;
using ProjectManagement.Contracts.Issue.Commands;

namespace ProjectManagement.Contracts.Bug.Commands
{
    public class CommentBug : CommentIssue
    {
        public CommentBug(Guid memberId, string content) : base(memberId, content)
        {
        }
    }
}
