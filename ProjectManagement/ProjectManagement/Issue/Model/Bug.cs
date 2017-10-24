using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.Exceptions;
using Infrastructure.Storage;
using Infrastructure.Storage.EF;
using ProjectManagement.Contracts.Bug.Events;
using ProjectManagement.Contracts.DomainExceptions;
using ProjectManagement.Contracts.Issue.Enums;
using ProjectManagement.Issue.Model.Abstract;
using ProjectManagement.Providers;
using ProjectManagement.Services;
using ProjectManagement.Sprint.Searchers;

namespace ProjectManagement.Issue.Model
{
    public class Bug : IEntity, IIssue
    {
        private Bug() : base() { }

        public Bug(Guid id, Guid projectId, string title, string description, IssueStatus status, Guid reporterId, Guid? assigneeId, DateTime createdAt, DateTime updatedAt)
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

        public Guid Id { get; private set; }
        public Guid ProjectId { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public IssueStatus Status { get; private set; }
        public Guid ReporterId { get; private set; }
        public Guid? AssigneeId { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }
        public Guid? SprintId { get; private set; }

        public ICollection<Comment.Comment> Comments { get; private set; }

        public ICollection<Label.Label> Labels { get; set; }

        public void AssignLabels(ICollection<Guid> requestedLabelsIds, ICollection<Label.Label> fetchedLabels)
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

        public void MarkAsInProgress()
        {
            if (SprintId == null)
                throw new IssueNotAssignedToSprint(Id, DomainInformationProvider.Name);

            if (Status != IssueStatus.Todo)
                throw new CannotChangeIssueStatus(Id, Status, IssueStatus.InProgress, DomainInformationProvider.Name);

            Status = IssueStatus.InProgress;
            UpdatedAt = DateTime.Now;
        }

        public void Comment(Guid memberId, string content, IAuthorizationService authorizationService)
        {
            System.Threading.Tasks.Task.Run(() => authorizationService.CheckUserMembership(memberId, ProjectId)).GetAwaiter().GetResult();
            Comments.Add(new Comment.Comment(memberId, content));
        }

        public void MarkAsDone()
        {
            if (SprintId == null)
                throw new IssueNotAssignedToSprint(Id, DomainInformationProvider.Name);

            if (Status != IssueStatus.InProgress)
                throw new CannotChangeIssueStatus(Id, Status, IssueStatus.Done, DomainInformationProvider.Name);

            Status = IssueStatus.Done;
            UpdatedAt = DateTime.Now;
        }

        public void AssignAssignee(User.Model.User assignee, IAuthorizationService authorizationService)
        {
            System.Threading.Tasks.Task.Run(() => authorizationService.CheckUserMembership(assignee.Id, ProjectId)).GetAwaiter().GetResult();
            AssigneeId = assignee.Id;
        }

        public void AssignToSprint(Guid sprintId, ISprintSearcher sprintSearcher)
        {
            System.Threading.Tasks.Task.Run(() => sprintSearcher.CheckIfSprintExistsInProjectScope(sprintId, ProjectId)).GetAwaiter().GetResult();
            SprintId = sprintId;
        }

        private void ValidateLabelsExistence(ICollection<Guid> requestedLabelsIds, ICollection<Label.Label> fetchedLabels)
        {
            var missingLabelsIds = requestedLabelsIds.Except(fetchedLabels.Select(x => x.Id)).ToList();

            if (missingLabelsIds.Count != 0)
                throw new EntitiesDoesNotExistInScope(missingLabelsIds, nameof(Label.Label), nameof(Project.Model.Project), ProjectId);
        }
    }
}
