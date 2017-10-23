using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Message;
using ProjectManagement.Contracts.Issue.Events;

namespace ProjectManagement.Contracts.Nfr.Events
{
    public class NfrCommented : IIssueCommented, IDomainEvent
    {
        public NfrCommented(Guid issueId, Guid commentId, string content, Guid memberId, DateTimeOffset addedAt)
        {
            IssueId = issueId;
            CommentId = commentId;
            Content = content;
            MemberId = memberId;
            AddedAt = addedAt;
        }

        public Guid IssueId { get; private set; }
        public Guid CommentId { get; private set; }
        public string Content { get; private set; }
        public Guid MemberId { get; private set; }
        public DateTimeOffset AddedAt { get; private set; }
        public long AggregateVersion { get; set; }
    }
}
