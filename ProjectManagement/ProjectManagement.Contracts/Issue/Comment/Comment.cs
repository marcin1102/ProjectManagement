using System;

namespace ProjectManagement.Contracts.Issue.Comment
{
    /// <summary>
    /// TODO: A comment should be an entity
    /// </summary>
    public class Comment
    {
        public Comment(Guid memberId, string content)
        {
            MemberId = memberId;
            Content = content;
        }

        public Guid MemberId { get; private set; }
        public string Content { get; private set; }
        public DateTime CreatedAt { get; set; }
    }
}
