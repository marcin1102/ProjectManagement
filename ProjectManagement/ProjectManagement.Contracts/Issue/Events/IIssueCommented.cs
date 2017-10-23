using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Message;

namespace ProjectManagement.Contracts.Issue.Events
{
    public interface IIssueCommented
    {
        Guid IssueId { get; }
        Guid CommentId { get; }
        string Content { get; }
        Guid MemberId { get; }
        DateTimeOffset AddedAt { get; }
    }
}