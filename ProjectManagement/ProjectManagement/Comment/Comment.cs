using System;
using Infrastructure.Storage.EF;

namespace ProjectManagement.Comment
{
    public class Comment : IEntity
    {
        public Comment(Guid memberId, string content)
        {
            Id = Guid.NewGuid();
            MemberId = memberId;
            Content = content;
            CreatedAt = DateTimeOffset.Now;
        }

        public Guid Id { get; private set; }
        public Guid MemberId { get; private set; }
        public string Content { get; private set; }
        public DateTimeOffset CreatedAt { get; set; }
    }
}
