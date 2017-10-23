using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementView.Storage.Models
{
    public class Comment
    {
        public Comment(Guid memberId, string content, DateTime addedAt)
        {
            MemberId = memberId;
            Content = content;
            AddedAt = addedAt;
        }

        public Guid MemberId { get; }
        public string Content { get; }
        public DateTime AddedAt { get; }
    }
}
