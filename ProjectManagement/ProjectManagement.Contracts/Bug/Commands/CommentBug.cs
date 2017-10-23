using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManagement.Contracts.Issue.Commands;

namespace ProjectManagement.Contracts.Bug.Commands
{
    public class CommentBug : CommentIssue
    {
        public CommentBug(Guid issueId, Guid memberId, string content) : base(issueId, memberId, content)
        {
        }
    }
}
