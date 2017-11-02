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
    public class Task : Abstract.AggregateIssue
    {
        public ICollection<ChildBug> Bugs { get; private set; }
        public ICollection<Subtask> Subtasks { get; private set; }

        protected Task() : base() { }

        public Task(Guid id, Guid projectId, string title, string description, IssueStatus status, Guid reporterId, Guid? assigneeId, DateTime createdAt, DateTime updatedAt) :
            base(id, projectId, title, description, status, reporterId, assigneeId, createdAt, updatedAt)
        {
            Bugs = new List<ChildBug>();
            Subtasks = new List<Subtask>();
        }

        public override void Created()
        {
            Update(new TaskCreated(Id, ProjectId, Title, Description, ReporterId, AssigneeId, Labels.Select(x => x.Id).ToList(), CreatedAt));
        }

        public override void AssignLabels(ICollection<Guid> requestedLabelsIds, ICollection<Label.Label> fetchedLabels)
        {
            base.AssignLabels(requestedLabelsIds, fetchedLabels);
            Update(new LabelAssignedToTask(Id, Labels.Select(x => x.Id).ToList()));
        }

        public override void MarkAsInProgress()
        {
            base.MarkAsInProgress();
            Update(new TaskMarkedAsInProgress(Id, Status));
        }

        public override async System.Threading.Tasks.Task Comment(Guid memberId, string content, IAuthorizationService authorizationService)
        {
            await base.Comment(memberId, content, authorizationService);
            var comment = Comments.OrderBy(x => x.CreatedAt).Last();
            Update(new TaskCommented(Id, comment.Id, comment.Content, comment.MemberId, comment.CreatedAt));
        }

        public override void MarkAsDone()
        {
            CheckIfRelatedIssuesAreDone();
            base.MarkAsDone();
            Update(new TaskMarkedAsDone(Id, Status));
        }

        private void CheckIfRelatedIssuesAreDone()
        {
            var areAllBugsDone = Bugs.All(x => x.Status == IssueStatus.Done);
            var areAllSubTasksDone = Subtasks.All(x => x.Status == IssueStatus.Done);
            if (!areAllBugsDone || !areAllSubTasksDone)
                throw new AllRelatedIssuesMustBeDone(Id, DomainInformationProvider.Name);
        }

        public override async System.Threading.Tasks.Task AssignAssignee(User.Model.User assignee, IAuthorizationService authorizationService)
        {
            await base.AssignAssignee(assignee, authorizationService);
            Update(new AssigneeAssignedToTask(Id, AssigneeId.Value));
        }

        public override async System.Threading.Tasks.Task AssignToSprint(Guid sprintId, ISprintSearcher sprintSearcher)
        {
            await base.AssignToSprint(sprintId, sprintSearcher);
            Update(new TaskAssignedToSprint(Id, SprintId.Value));
        }

        private ChildBug GetBugWithId(Guid bugId)
        {
            var bug = Bugs.SingleOrDefault(x => x.Id == bugId);
            if (bug == null)
                throw new EntityDoesNotExistsInScope(bugId, nameof(ChildBug), nameof(Task), Id);
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

        public async System.Threading.Tasks.Task CommentBug(Guid bugId, Guid memberId, string content, IAuthorizationService authorizationService)
        {
            var bug = GetBugWithId(bugId);

            await bug.Comment(memberId, content, authorizationService);
            var newComment = bug.Comments.OrderBy(x => x.CreatedAt).Last();
            Update(new BugCommented(bugId, newComment.Id, newComment.Content, newComment.MemberId, newComment.CreatedAt));
        }

        public void MarkBugAsDone(Guid bugId)
        {
            var bug = GetBugWithId(bugId);
            bug.MarkAsDone();
            Update(new BugMarkedAsDone(bugId, Contracts.Issue.Enums.IssueStatus.Done));
        }

        public async System.Threading.Tasks.Task AssignAssigneeToBug(Guid bugId, User.Model.User assignee, IAuthorizationService authorizationService)
        {
            var bug = Bugs.Single(x => x.Id == bugId);
            await bug.AssignAssignee(assignee, authorizationService);
            Update(new AssigneeAssignedToBug(bug.Id, assignee.Id));
        }

        public async System.Threading.Tasks.Task AssignBugToSprint(Guid bugId, Guid sprintId, ISprintSearcher sprintSearcher)
        {
            var bug = Bugs.Single(x => x.Id == bugId);
            await bug.AssignToSprint(sprintId, sprintSearcher);
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

        public async System.Threading.Tasks.Task CommentSubtask(Guid subtaskId, Guid memberId, string content, IAuthorizationService authorizationService)
        {
            var subtask = GetSubtaskWithId(subtaskId);
            await subtask.Comment(memberId, content, authorizationService);
            var newComment = subtask.Comments.OrderBy(x => x.CreatedAt).Last();
            Update(new SubtaskCommented(subtaskId, newComment.Id, newComment.Content, newComment.MemberId, newComment.CreatedAt));
        }

        public void MarkSubtaskAsDone(Guid subtaskId)
        {
            var subtask = GetSubtaskWithId(subtaskId);
            subtask.MarkAsDone();
            Update(new SubtaskMarkedAsDone(subtaskId, Contracts.Issue.Enums.IssueStatus.Done));
        }

        public async System.Threading.Tasks.Task AssignAssigneeToSubtask(Guid subtaskId, User.Model.User assignee, IAuthorizationService authorizationService)
        {
            var subtask = GetSubtaskWithId(subtaskId);
            await subtask.AssignAssignee(assignee, authorizationService);
            Update(new AssigneeAssignedToSubtask(subtask.Id, assignee.Id));
        }

        public async System.Threading.Tasks.Task AssignSubtaskToSprint(Guid subtaskId, Guid sprintId, ISprintSearcher sprintSearcher)
        {
            var subtask = GetSubtaskWithId(subtaskId);
            await subtask.AssignToSprint(sprintId, sprintSearcher);
            Update(new SubtaskAssignedToSprint(subtask.Id, sprintId));
        }
#endregion
    }
}
