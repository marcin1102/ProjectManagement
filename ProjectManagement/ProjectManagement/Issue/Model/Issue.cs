using Infrastructure.Storage;
using Newtonsoft.Json;
using ProjectManagement.Contracts.Issue.Enums;
using System;
using System.Collections.Generic;
using ProjectManagement.Contracts.Issue.Events;
using System.Linq;

namespace ProjectManagement.Issue.Model
{
    public class Issue : AggregateRoot
    {
        public Guid ProjectId { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public IssueType Type { get; private set; }
        public Status Status { get; private set; }
        public User.Model.User Reporter { get; private set; }
        public User.Model.User Assignee { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        public string comments { get; private set; }
        public ICollection<Comment.Comment> Comments {
            get => JsonConvert.DeserializeObject<List<Comment.Comment>>(comments);
            set {
                comments = JsonConvert.SerializeObject(value);
            }
        }

        private List<IssueSubtasks.IssueSubtask> subtasks;
        public IEnumerable<IssueSubtasks.IssueSubtask> Subtasks => subtasks;

        private List<IssueLabel.IssueLabel> labels;
        public IEnumerable<IssueLabel.IssueLabel> Labels => labels;

        private Issue() { }

        public Issue(Guid id, Guid projectId, string title, string description, IssueType type, Status status, User.Model.User reporter, User.Model.User assignee, DateTime createdAt, DateTime updatedAt) : base(id)
        {
            ProjectId = projectId;
            Title = title;
            Description = description;
            Type = type;
            Status = status;
            Reporter = reporter;
            Assignee = assignee;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            Comments = new List<Comment.Comment>();
            subtasks = new List<IssueSubtasks.IssueSubtask>();
            labels = new List<IssueLabel.IssueLabel>();
        }

        public override void Created()
        {
            Update(new IssueCreated(Id, ProjectId, Title, Description, Type, Reporter.Id, Assignee?.Id, labels.Select(x => x.Id).ToList()));
        }

        public void AssignLabels(ICollection<Guid> labelsIds)
        {
            foreach (var labelId in labelsIds)
            {
                labels.Add(new IssueLabel.IssueLabel(Id, labelId));
            }
        }
    }
}
