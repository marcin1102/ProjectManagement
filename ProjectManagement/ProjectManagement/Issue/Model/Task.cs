using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.Exceptions;
using Infrastructure.Storage;
using ProjectManagement.Contracts.Bug.Events;
using ProjectManagement.Contracts.DomainExceptions;
using ProjectManagement.Contracts.Issue.Enums;
using ProjectManagement.Contracts.Subtask.Events;
using ProjectManagement.Contracts.Task.Commands;
using ProjectManagement.Contracts.Task.Events;
using ProjectManagement.Issue.Factory;
using ProjectManagement.Issue.Model.Abstract;
using ProjectManagement.Providers;
using ProjectManagement.Services;
using ProjectManagement.Sprint.Searchers;

namespace ProjectManagement.Issue.Model
{
    public class Task : AggregateRoot, IIssue
    {
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
        public ICollection<Bug> Bugs { get; private set; }
        public ICollection<Subtask> Subtasks { get; private set; }

        protected Task() { }

        public Task(Guid id, Guid projectId, string title, string description, IssueStatus status, Guid reporterId, Guid? assigneeId, DateTime createdAt, DateTime updatedAt) : base(id)
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
            Bugs = new List<Bug>();
            Subtasks = new List<Subtask>();
        }

        public override void Created()
        {
            Update(new TaskCreated(Id, ProjectId, Title, Description, ReporterId, AssigneeId, Labels.Select(x => x.Id).ToList(), CreatedAt));
        }

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

            Update(new LabelAssignedToTask(Id, Labels.Select(x => x.Id).ToList()));
        }

        public void MarkAsInProgress()
        {
            if (SprintId == null)
                throw new IssueNotAssignedToSprint(Id, DomainInformationProvider.Name);

            if (Status != IssueStatus.Todo)
                throw new CannotChangeIssueStatus(Id, Status, IssueStatus.InProgress, DomainInformationProvider.Name);

            Status = IssueStatus.InProgress;
            UpdatedAt = DateTime.Now;
            Update(new TaskMarkedAsInProgress(Id, Status));
        }

        public void Comment(Guid memberId, string content, IAuthorizationService authorizationService)
        {
            System.Threading.Tasks.Task.Run(() => authorizationService.CheckUserMembership(memberId, ProjectId)).GetAwaiter().GetResult();
            var comment = new Comment.Comment(memberId, content);
            if (Comments == null)
                Comments = new List<Comment.Comment>();
            Comments.Add(comment);
            Update(new TaskCommented(Id, comment.Id, comment.Content, comment.MemberId, comment.CreatedAt));
        }

        public void MarkAsDone()
        {
            CheckIfRelatedIssuesAreDone();

            if (SprintId == null)
                throw new IssueNotAssignedToSprint(Id, DomainInformationProvider.Name);

            if (Status != IssueStatus.InProgress)
                throw new CannotChangeIssueStatus(Id, Status, IssueStatus.Done, DomainInformationProvider.Name);

            Status = IssueStatus.Done;
            UpdatedAt = DateTime.Now;
            Update(new TaskMarkedAsDone(Id, Status));
        }

        private void CheckIfRelatedIssuesAreDone()
        {
            var areAllBugsDone = Bugs.All(x => x.Status == IssueStatus.Done);
            var areAllSubTasksDone = Subtasks.All(x => x.Status == IssueStatus.Done);
            if (!areAllBugsDone || !areAllSubTasksDone)
                throw new AllRelatedIssuesMustBeDone(Id, DomainInformationProvider.Name);
        }

        public void AssignAssignee(User.Model.User assignee, IAuthorizationService authorizationService)
        {
            System.Threading.Tasks.Task.Run(() => authorizationService.CheckUserMembership(assignee.Id, ProjectId)).GetAwaiter().GetResult();
            AssigneeId = assignee.Id;
            Update(new AssigneeAssignedToTask(Id, AssigneeId.Value));
        }

        public void AssignToSprint(Guid sprintId, ISprintSearcher sprintSearcher)
        {
            System.Threading.Tasks.Task.Run(() => sprintSearcher.CheckIfSprintExistsInProjectScope(sprintId, ProjectId)).GetAwaiter().GetResult();
            SprintId = sprintId;
            Update(new TaskAssignedToSprint(Id, SprintId.Value));
        }

        private void ValidateLabelsExistence(ICollection<Guid> requestedLabelsIds, ICollection<Label.Label> fetchedLabels)
        {
            var missingLabelsIds = requestedLabelsIds.Except(fetchedLabels.Select(x => x.Id)).ToList();

            if (missingLabelsIds.Count != 0)
                throw new EntitiesDoesNotExistInScope(missingLabelsIds, nameof(Label.Label), nameof(Project.Model.Project), ProjectId);
        }

        private Bug GetBugWithId(Guid bugId)
        {
            var bug = Bugs.SingleOrDefault(x => x.Id == bugId);
            if (bug == null)
                throw new EntityDoesNotExistsInScope(bugId, nameof(Bug), nameof(Task), Id);
            return bug;
        }

        private Subtask GetSubtaskWithId(Guid subtaskId)
        {
            var subtask = Subtasks.SingleOrDefault(x => x.Id == subtaskId);
            if (subtask == null)
                throw new EntityDoesNotExistsInScope(subtaskId, nameof(Subtask), nameof(Task), Id);
            return subtask;
        }

        #region Bug
        public void AddBug(IIssueFactory issueFactory, AddBugToTask command)
        {
            var bug = System.Threading.Tasks.Task.Run(() => issueFactory.GenerateBug(command)).GetAwaiter().GetResult();
            Bugs.Add(bug);
            Update(new BugAddedToTask(bug.Id, Id, ProjectId, bug.Title, bug.Description, bug.ReporterId, bug.AssigneeId, bug.Labels.Select(x => x.Id).ToList(), bug.CreatedAt));
        }
        public void AssignLabelsToBug(Guid bugId, ICollection<Guid> requestedLabelsIds, ICollection<Label.Label> fetchedLabels)
        {
            var bug = GetBugWithId(bugId);

            bug.AssignLabels(requestedLabelsIds, fetchedLabels);
            Update(new LabelAssignedToBug(bugId, bug.Labels.Select(x => x.Id).ToList()));
        }

        public void MarkBugAsInProgress(Guid bugId)
        {
            var bug = GetBugWithId(bugId);

            bug.MarkAsInProgress();
            Update(new BugMarkedAsInProgress(bugId, Contracts.Issue.Enums.IssueStatus.InProgress));
        }

        public void CommentBug(Guid bugId, Guid memberId, string content, IAuthorizationService authorizationService)
        {
            var bug = GetBugWithId(bugId);

            bug.Comment(memberId, content, authorizationService);
            var newComment = bug.Comments.OrderBy(x => x.CreatedAt).Last();
            Update(new BugCommented(bugId, newComment.Id, newComment.Content, newComment.MemberId, newComment.CreatedAt));
        }

        public void MarkBugAsDone(Guid bugId)
        {
            var bug = GetBugWithId(bugId);
            bug.MarkAsDone();
            Update(new BugMarkedAsDone(bugId, Contracts.Issue.Enums.IssueStatus.Done));
        }

        public void AssignAssigneeToBug(Guid bugId, User.Model.User assignee, IAuthorizationService authorizationService)
        {
            var bug = Bugs.Single(x => x.Id == bugId);
            bug.AssignAssignee(assignee, authorizationService);
            Update(new AssigneeAssignedToBug(bug.Id, assignee.Id));
        }

        public void AssignBugToSprint(Guid bugId, Guid sprintId, ISprintSearcher sprintSearcher)
        {
            var bug = Bugs.Single(x => x.Id == bugId);
            bug.AssignToSprint(sprintId, sprintSearcher);
            Update(new BugAssignedToSprint(bug.Id, sprintId));
        }
#endregion

        #region Subtask
        public void AddSubtask(IIssueFactory issueFactory, AddSubtaskToTask command)
        {
            var subtask = System.Threading.Tasks.Task.Run(() => issueFactory.GenerateSubtask(command)).GetAwaiter().GetResult();
            Subtasks.Add(subtask);
            Update(new SubtaskAddedToTask(subtask.Id, Id, ProjectId, subtask.Title, subtask.Description,
                subtask.ReporterId, subtask.AssigneeId, subtask.Labels.Select(x => x.Id).ToList(), subtask.CreatedAt));
        }

        public void AssignLabelsToSubtask(Guid subtaskId, ICollection<Guid> requestedLabelsIds, ICollection<Label.Label> fetchedLabels)
        {
            var subtask = GetSubtaskWithId(subtaskId);
            subtask.AssignLabels(requestedLabelsIds, fetchedLabels);
            Update(new LabelAssignedToSubtask(subtaskId, subtask.Labels.Select(x => x.Id).ToList()));
        }

        public void MarkSubtaskAsInProgress(Guid subtaskId)
        {
            var subtask = GetSubtaskWithId(subtaskId);
            subtask.MarkAsInProgress();
            Update(new SubtaskMarkedAsInProgress(subtaskId, Contracts.Issue.Enums.IssueStatus.InProgress));
        }

        public void CommentSubtask(Guid subtaskId, Guid memberId, string content, IAuthorizationService authorizationService)
        {
            var subtask = GetSubtaskWithId(subtaskId);
            subtask.Comment(memberId, content, authorizationService);
            var newComment = subtask.Comments.OrderBy(x => x.CreatedAt).Last();
            Update(new SubtaskCommented(subtaskId, newComment.Id, newComment.Content, newComment.MemberId, newComment.CreatedAt));
        }

        public void MarkSubtaskAsDone(Guid subtaskId)
        {
            var subtask = GetSubtaskWithId(subtaskId);
            subtask.MarkAsDone();
            Update(new SubtaskMarkedAsDone(subtaskId, Contracts.Issue.Enums.IssueStatus.Done));
        }

        public void AssignAssigneeToSubtask(Guid subtaskId, User.Model.User assignee, IAuthorizationService authorizationService)
        {
            var subtask = GetSubtaskWithId(subtaskId);
            subtask.AssignAssignee(assignee, authorizationService);
            Update(new AssigneeAssignedToSubtask(subtask.Id, assignee.Id));
        }

        public void AssignSubtaskToSprint(Guid subtaskId, Guid sprintId, ISprintSearcher sprintSearcher)
        {
            var subtask = GetSubtaskWithId(subtaskId);
            subtask.AssignToSprint(sprintId, sprintSearcher);
            Update(new SubtaskAssignedToSprint(subtask.Id, sprintId));
        }
#endregion
    }
}
