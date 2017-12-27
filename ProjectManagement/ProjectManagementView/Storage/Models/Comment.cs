using System;

namespace ProjectManagementView.Storage.Models
{
    public class Comment
    {
        public Comment(Guid commentId, Guid memberId, string content, DateTimeOffset addedAt)
        {
            CommentId = commentId;
            MemberId = memberId;
            Content = content;
            AddedAt = addedAt;
        }

        public Guid CommentId { get; }
        public Guid MemberId { get; }
        public string Content { get; }
        public DateTimeOffset AddedAt { get; }
    }
}
