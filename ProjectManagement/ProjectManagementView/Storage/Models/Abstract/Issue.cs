using System;
using Infrastructure.Storage.EF;
using ProjectManagement.Contracts.Issue.Enums;

namespace ProjectManagementView.Storage.Models.Abstract
{
    public abstract class Issue : IEntity
    {
        public Issue(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IssueStatus Status { get; set; }
        //public User.Model.User Reporter { get; private set; }
        //public User.Model.User Assignee { get; private set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public long Version { get; set; }
    }
}
