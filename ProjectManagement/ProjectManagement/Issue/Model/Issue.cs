using Infrastructure.Storage;
using Newtonsoft.Json;
using ProjectManagement.Contracts.Issue.Enums;
using System;
using System.Collections.Generic;
using ProjectManagement.Contracts.Issue.Events;
using System.Linq;
using ProjectManagement.Contracts.Issue.Comment;
using ProjectManagement.Contracts.Exceptions;
using ProjectManagement.Providers;
using ProjectManagement.IssueSubtasks;

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
        public ICollection<Comment> Comments {
            get => JsonConvert.DeserializeObject<List<Comment>>(comments);
            set {
                comments = JsonConvert.SerializeObject(value);
            }
        }

        private ICollection<IssueSubtasks.IssueSubtask> subtasks;
        public IEnumerable<IssueSubtasks.IssueSubtask> Subtasks => subtasks;

        private ICollection<IssueLabel.IssueLabel> labels;
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
            Comments = new List<Comment>();
            subtasks = new List<IssueSubtasks.IssueSubtask>();
            labels = new List<IssueLabel.IssueLabel>();
        }

        public override void Created()
        {
            Update(new IssueCreated(Id, ProjectId, Title, Description, Type, Reporter.Id, Assignee?.Id, labels.Select(x => x.Id).ToList()));
        }

        public void AssignLabels(ICollection<Guid> labelsIds)
        {
            var labelsIdsToAdd = labelsIds.Except(labels.Select(x => x.LabelId));
            var labelsIdsToRemove = labels.Select(x => x.LabelId).Except(labelsIds).ToList();
            var labelsToRemove = labels.Where(x => labelsIdsToRemove.Any(y => x.LabelId == y)).ToList();

            foreach (var add in labelsIdsToAdd)
            {
                labels.Add(new IssueLabel.IssueLabel(Id, add));
            }
            foreach (var remove in labelsToRemove)
            {
                labels.Remove(remove);
            }
        }

        public void Comment(Comment comment)
        {
            var commentsToUpdate = Comments.ToList();
            comment.CreatedAt = DateTime.Now;
            commentsToUpdate.Add(comment);
            Comments = commentsToUpdate;
        }

        public void AddSubtask(Guid subtaskId, IssueType subtaskType)
        {
            CheckIfSubtaskCanBeAdded(subtaskId, subtaskType);
            subtasks.Add(new IssueSubtask(ProjectId, Id, subtaskId));
        }

        private void CheckIfSubtaskCanBeAdded(Guid subtaskId, IssueType subtaskType)
        {
            if (Type == IssueType.Bug)
                throw new CannotAddSubtaskToBug(DomainInformationProvider.Name);

            if (Type == IssueType.Nfr && subtaskType != IssueType.Bug)
                throw new NfrsCanHaveOnlyBugs(DomainInformationProvider.Name);

            if (Type == IssueType.Task && subtaskType == IssueType.Nfr)
                throw new CannotAddNfrAsSubtask(DomainInformationProvider.Name);

            if (subtasks.Any(x => x.SubtaskId == subtaskId))
                throw new IssueIsSubtaskAlready(Id, subtaskId, "ProjectManagement");
        }
    }
}
