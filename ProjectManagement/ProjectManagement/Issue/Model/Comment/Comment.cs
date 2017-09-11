using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManagement.Comment
{
    public class Comment
    {
        public Comment(Guid memberId, string content)
        {
            MemberId = memberId;
            Content = content;
        }

        public Guid MemberId { get; private set; }
        public string Content { get; private set; }
        public DateTime CreatedAt { get; private set; }
    }
}
