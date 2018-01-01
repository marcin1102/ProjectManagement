using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManagement.Infrastructure.Primitives.Exceptions;
using ProjectManagement.Infrastructure.Storage;
using ProjectManagement.Infrastructure.Storage.EF;
using ProjectManagement.Contracts.DomainExceptions;
using ProjectManagement.Contracts.Issue.Enums;
using ProjectManagement.Providers;
using ProjectManagement.Services;
using ProjectManagement.Sprint.Searchers;

namespace ProjectManagement.Issue.Model.Abstract
{
    public abstract class AggregateIssue : AggregateRoot , IIssue
    {
        public Guid ProjectId { get; protected set; }
        public string Title { get; protected set; }
        public string Description { get; protected set; }
        public IssueStatus Status { get; protected set; }
        public Guid ReporterId { get; protected set; }
        public Guid? AssigneeId { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime UpdatedAt { get; protected set; }
        public Guid? SprintId { get; protected set; }

        public ICollection<Comment.Comment> Comments { get; protected set; }
        public ICollection<Label.Label> Labels { get; set; }

        protected AggregateIssue() { }

        public AggregateIssue(Guid id, Guid projectId, string title, string description, IssueStatus status, Guid reporterId, Guid? assigneeId, DateTime createdAt, DateTime updatedAt) : base(id)
        {
            ProjectId = projectId;
            Title = title;
            Description = description;
            Status = status;
            ReporterId = reporterId;
            AssigneeId = assigneeId;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            Comments = new List<Comment.Comment>();
            Labels = new List<Label.Label>();
        }

        public virtual void AssignLabels(ICollection<Guid> requestedLabelsIds, ICollection<Label.Label> fetchedLabels)
        {
            ValidateLabelsExistence(requestedLabelsIds, fetchedLabels);

            var labelsIdsCurentlyInUse = Labels.Select(x => x.Id);

            var labelsIdsToAdd = requestedLabelsIds.Except(labelsIdsCurentlyInUse);
            var labelsToAdd = fetchedLabels.Where(x => labelsIdsToAdd.Contains(x.Id));
            var labelsIdsToRemove = labelsIdsCurentlyInUse.Except(requestedLabelsIds);
            var labelsToRemove = Labels.Where(x => labelsIdsToRemove.Contains(x.Id)).ToList();

            foreach (var add in labelsToAdd)
            {
                Labels.Add(add);
            }
            foreach (var remove in labelsToRemove)
            {
                Labels.Remove(remove);
            }
        }

        public virtual async System.Threading.Tasks.Task MarkAsInProgress(Guid memberId, IMembershipService authorizationService)
        {
            await authorizationService.CheckUserMembership(memberId, ProjectId);
            if (SprintId == null)
                throw new IssueNotAssignedToSprint(Id, DomainInformationProvider.Name);

            if (Status != IssueStatus.Todo)
                throw new CannotChangeIssueStatus(Id, Status, IssueStatus.InProgress, DomainInformationProvider.Name);

            Status = IssueStatus.InProgress;
            UpdatedAt = DateTime.UtcNow;
        }

        public virtual async System.Threading.Tasks.Task Comment(Guid memberId, string content, IMembershipService authorizationService)
        {
            await authorizationService.CheckUserMembership(memberId, ProjectId);
            var comment = new Comment.Comment(memberId, content);
            if (Comments == null)
                Comments = new List<Comment.Comment>();
            Comments.Add(comment);
        }

        public virtual async System.Threading.Tasks.Task MarkAsDone(Guid memberId, IMembershipService authorizationService)
        {
            await authorizationService.CheckUserMembership(memberId, ProjectId);
            if (SprintId == null)
                throw new IssueNotAssignedToSprint(Id, DomainInformationProvider.Name);

            if (Status != IssueStatus.InProgress)
                throw new CannotChangeIssueStatus(Id, Status, IssueStatus.Done, DomainInformationProvider.Name);

            Status = IssueStatus.Done;
            UpdatedAt = DateTime.UtcNow;
        }

        public virtual async System.Threading.Tasks.Task AssignAssignee(User.Model.Member assignee, IMembershipService authorizationService)
        {
            await authorizationService.CheckUserMembership(assignee.Id, ProjectId);
            AssigneeId = assignee.Id;
        }

        public virtual async System.Threading.Tasks.Task AssignToSprint(Guid sprintId, ISprintSearcher sprintSearcher)
        {
            await sprintSearcher.CheckIfSprintExistsInProjectScope(sprintId, ProjectId);
            SprintId = sprintId;
        }

        protected void ValidateLabelsExistence(ICollection<Guid> requestedLabelsIds, ICollection<Label.Label> fetchedLabels)
        {
            var missingLabelsIds = requestedLabelsIds.Except(fetchedLabels.Select(x => x.Id)).ToList();

            if (missingLabelsIds.Count != 0)
                throw new EntitiesDoesNotExistInScope(missingLabelsIds, nameof(Label.Label), nameof(Project.Model.Project), ProjectId);
        }
    }

    public abstract class Issue : IIssue, IEntity
    {
        public Guid Id { get; private set; }
        public Guid ProjectId { get; protected set; }
        public string Title { get; protected set; }
        public string Description { get; protected set; }
        public IssueStatus Status { get; protected set; }
        public Guid ReporterId { get; protected set; }
        public Guid? AssigneeId { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime UpdatedAt { get; protected set; }
        public Guid? SprintId { get; protected set; }

        public ICollection<Comment.Comment> Comments { get; protected set; }
        public ICollection<Label.Label> Labels { get; set; }

        protected Issue() { }

        public Issue(Guid id, Guid projectId, string title, string description, IssueStatus status, Guid reporterId, Guid? assigneeId, DateTime createdAt, DateTime updatedAt)
        {
            Id = id;
            ProjectId = projectId;
            Title = title;
            Description = description;
            Status = status;
            ReporterId = reporterId;
            AssigneeId = assigneeId;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            Comments = new List<Comment.Comment>();
            Labels = new List<Label.Label>();
        }

        public virtual void AssignLabels(ICollection<Guid> requestedLabelsIds, ICollection<Label.Label> fetchedLabels)
        {
            ValidateLabelsExistence(requestedLabelsIds, fetchedLabels);

            var labelsIdsCurentlyInUse = Labels.Select(x => x.Id);

            var labelsIdsToAdd = requestedLabelsIds.Except(labelsIdsCurentlyInUse);
            var labelsToAdd = fetchedLabels.Where(x => labelsIdsToAdd.Contains(x.Id));
            var labelsIdsToRemove = labelsIdsCurentlyInUse.Except(requestedLabelsIds);
            var labelsToRemove = Labels.Where(x => labelsIdsToRemove.Contains(x.Id)).ToList();

            foreach (var add in labelsToAdd)
            {
                Labels.Add(add);
            }
            foreach (var remove in labelsToRemove)
            {
                Labels.Remove(remove);
            }
        }

        public virtual async System.Threading.Tasks.Task MarkAsInProgress(Guid memberId, IMembershipService authorizationService)
        {
            await authorizationService.CheckUserMembership(memberId, ProjectId);
            if (SprintId == null)
                throw new IssueNotAssignedToSprint(Id, DomainInformationProvider.Name);

            if (Status != IssueStatus.Todo)
                throw new CannotChangeIssueStatus(Id, Status, IssueStatus.InProgress, DomainInformationProvider.Name);

            Status = IssueStatus.InProgress;
            UpdatedAt = DateTime.UtcNow;
        }

        public virtual async System.Threading.Tasks.Task Comment(Guid memberId, string content, IMembershipService authorizationService)
        {
            await authorizationService.CheckUserMembership(memberId, ProjectId);
            var comment = new Comment.Comment(memberId, content);
            if (Comments == null)
                Comments = new List<Comment.Comment>();
            Comments.Add(comment);
        }

        public virtual async System.Threading.Tasks.Task MarkAsDone(Guid memberId, IMembershipService authorizationService)
        {
            await authorizationService.CheckUserMembership(memberId, ProjectId);
            if (SprintId == null)
                throw new IssueNotAssignedToSprint(Id, DomainInformationProvider.Name);

            if (Status != IssueStatus.InProgress)
                throw new CannotChangeIssueStatus(Id, Status, IssueStatus.Done, DomainInformationProvider.Name);

            Status = IssueStatus.Done;
            UpdatedAt = DateTime.UtcNow;
        }

        public virtual async System.Threading.Tasks.Task AssignAssignee(User.Model.Member assignee, IMembershipService authorizationService)
        {
            await authorizationService.CheckUserMembership(assignee.Id, ProjectId);
            AssigneeId = assignee.Id;
        }

        public virtual async System.Threading.Tasks.Task AssignToSprint(Guid sprintId, ISprintSearcher sprintSearcher)
        {
            await sprintSearcher.CheckIfSprintExistsInProjectScope(sprintId, ProjectId);
            SprintId = sprintId;
        }

        protected void ValidateLabelsExistence(ICollection<Guid> requestedLabelsIds, ICollection<Label.Label> fetchedLabels)
        {
            var missingLabelsIds = requestedLabelsIds.Except(fetchedLabels.Select(x => x.Id)).ToList();

            if (missingLabelsIds.Count != 0)
                throw new EntitiesDoesNotExistInScope(missingLabelsIds, nameof(Label.Label), nameof(Project.Model.Project), ProjectId);
        }
    }
}
