using System;
using System.Collections.Generic;
using ProjectManagement.Infrastructure.Storage.EF;
using Newtonsoft.Json;
using ProjectManagement.Contracts.Issue.Enums;

namespace ProjectManagementView.Storage.Models.Abstract
{
    public abstract class Issue : IEntity
    {
        internal Issue()
        {

        }

        public Issue(Guid id)
        {
            Id = id; ;
            Labels = new List<Label>();
            Comments = new List<Comment>();
        }

        public Guid Id { get; set; }
        public Guid ProjectId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IssueStatus Status { get; set; }
        public User Reporter { get; set; }
        public User Assignee { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        private string labels { get; set; }
        public ICollection<Label> Labels
        {
            get => JsonConvert.DeserializeObject<ICollection<Label>>(labels);
            set
            {
                labels = JsonConvert.SerializeObject(value);
            }
        }
        private string comments { get; set; }
        public ICollection<Comment> Comments
        {
            get => JsonConvert.DeserializeObject<ICollection<Comment>>(comments);
            set
            {
                comments = JsonConvert.SerializeObject(value);
            }
        }
        public long Version { get; set; }
    }
}
