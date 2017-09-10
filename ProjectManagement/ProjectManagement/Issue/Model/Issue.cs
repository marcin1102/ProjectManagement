using Infrastructure.Storage;
using Newtonsoft.Json;
using ProjectManagement.Contracts.Issue.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManagement.Issue.Model
{
    public class Issue : AggregateRoot
    {
        public string Title { get; private set; }
        public string Description { get; private set; }
        public IssueType Type { get; private set; }
        public Status Status { get; private set; }
        public User.Model.User Reporter { get; private set; }
        public User.Model.User Assignee { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        private string comments;
        public ICollection<Comment.Comment> Comments {
            get => JsonConvert.DeserializeObject<ICollection<Comment.Comment>>(comments);
            set {
                comments = JsonConvert.SerializeObject(value);
            }
        }
    }
}
