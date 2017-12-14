using System;

namespace ProjectManagementView.Storage.Models
{
    public class Comment
    {
        public Comment(Guid memberId, string content, DateTimeOffset addedAt)
        {
            MemberId = memberId;
            Content = content;
            AddedAt = addedAt;
        }

        public Guid MemberId { get; }
        public string Content { get; }
        public DateTimeOffset AddedAt { get; }
    }
}
