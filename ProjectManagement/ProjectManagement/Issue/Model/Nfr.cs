using System;
using System.Collections.Generic;
using System.Linq;
using ProjectManagement.Infrastructure.Primitives.Exceptions;
using ProjectManagement.Infrastructure.Storage;
using ProjectManagement.Contracts.Bug.Events;
using ProjectManagement.Contracts.DomainExceptions;
using ProjectManagement.Contracts.Issue.Enums;
using ProjectManagement.Contracts.Nfr.Commands;
using ProjectManagement.Contracts.Nfr.Events;
using ProjectManagement.Issue.Factory;
using ProjectManagement.Issue.Model.Abstract;
using ProjectManagement.Providers;
using ProjectManagement.Services;
using ProjectManagement.Sprint.Searchers;

namespace ProjectManagement.Issue.Model
{
    public class Nfr : Abstract.AggregateIssue
    {
        private Nfr() : base() { }

        public Nfr(Guid id, Guid projectId, string title, string description, IssueStatus status, Guid reporterId, Guid? assigneeId, DateTime createdAt, DateTime updatedAt) :
            base(id, projectId, title, description, status, reporterId, assigneeId, createdAt, updatedAt)
        {
            Bugs = new List<ChildBug>();
        }

        public ICollection<ChildBug> Bugs { get; private set; }

        public override void Created()
        {
            Update(new NfrCreated(Id, ProjectId, Title, Description, ReporterId, AssigneeId, Labels.Select(x => x.Id).ToList(), CreatedAt));
        }

        public override void AssignLabels(ICollection<Guid> requestedLabelsIds, ICollection<Label.Label> fetchedLabels)
        {
            base.AssignLabels(requestedLabelsIds, fetchedLabels);
            Update(new LabelAssignedToNfr(Id, Labels.Select(x => x.Id).ToList()));
        }

        public override async System.Threading.Tasks.Task MarkAsInProgress(Guid memberId, IMembershipService authorizationService)
        {
            await base.MarkAsInProgress(memberId, authorizationService);
            Update(new NfrMarkedAsInProgress(Id, Status));
        }

        public override async System.Threading.Tasks.Task Comment(Guid memberId, string content, IMembershipService authorizationService)
        {
            await base.Comment(memberId, content, authorizationService);
            var comment = Comments.OrderBy(x => x.CreatedAt).Last();
            Update(new NfrCommented(Id, comment.Id, comment.Content, comment.MemberId, comment.CreatedAt));
        }

        public override async System.Threading.Tasks.Task MarkAsDone(Guid memberId, IMembershipService authorizationService)
        {
            CheckIfRelatedIssuesAreDone();
            await base.MarkAsDone(memberId, authorizationService);
            Update(new NfrMarkedAsDone(Id, Status));
        }

        private void CheckIfRelatedIssuesAreDone()
        {
            var areAllBugsDone = Bugs.All(x => x.Status == IssueStatus.Done);
            if (!areAllBugsDone)
                throw new AllRelatedIssuesMustBeDone(Id, DomainInformationProvider.Name);
        }

        public override async System.Threading.Tasks.Task AssignAssignee(User.Model.Member assignee, IMembershipService authorizationService)
        {
            await base.AssignAssignee(assignee, authorizationService);
            Update(new AssigneeAssignedToNfr(Id, AssigneeId.Value));
        }

        public override async System.Threading.Tasks.Task AssignToSprint(Guid sprintId, ISprintSearcher sprintSearcher)
        {
            await base.AssignToSprint(sprintId, sprintSearcher);
            Update(new NfrAssignedToSprint(Id, SprintId.Value));
        }


        private ChildBug GetBugWithId(Guid bugId)
        {
            var bug = Bugs.SingleOrDefault(x => x.Id == bugId);
            if (bug == null)
                throw new EntityDoesNotExistsInScope(bugId, nameof(ChildBug), nameof(Nfr), Id);
            return bug;
        }

        #region Bug
        public async System.Threading.Tasks.Task AddBug(IIssueFactory issueFactory, AddBugToNfr command)
        {
            if (Status == IssueStatus.Done)
                throw new CannotAddChildIssueWhenParentIssueIsDone<Nfr, ChildBug>(Id);

            var bug = await issueFactory.GenerateChildBug(command);
            Bugs.Add(bug);
            Update(new BugAddedToNfr(bug.Id, Id, ProjectId, bug.Title, bug.Description, bug.ReporterId, bug.AssigneeId, bug.Labels.Select(x => x.Id).ToList(), bug.CreatedAt));
        }
        public void AssignLabelsToBug(Guid bugId, ICollection<Guid> requestedLabelsIds, ICollection<Label.Label> fetchedLabels)
        {
            var bug = GetBugWithId(bugId);
            bug.AssignLabels(requestedLabelsIds, fetchedLabels);
            Update(new LabelAssignedToBug(bugId, bug.Labels.Select(x => x.Id).ToList()));
        }

        public async System.Threading.Tasks.Task MarkBugAsInProgress(Guid bugId, Guid memberId, IMembershipService authorizationService)
        {
            var bug = Bugs.Single(x => x.Id == bugId);
            await bug.MarkAsInProgress(memberId, authorizationService);
            Update(new BugMarkedAsInProgress(bugId, Contracts.Issue.Enums.IssueStatus.InProgress));
        }

        public async System.Threading.Tasks.Task CommentBug(Guid bugId, Guid memberId, string content, IMembershipService authorizationService)
        {
            var bug = Bugs.Single(x => x.Id == bugId);
            await bug.Comment(memberId, content, authorizationService);
            var newComment = bug.Comments.OrderBy(x => x.CreatedAt).Last();
            Update(new BugCommented(bugId, newComment.Id, newComment.Content, newComment.MemberId, newComment.CreatedAt));
        }

        public async System.Threading.Tasks.Task MarkBugAsDone(Guid bugId, Guid memberId, IMembershipService authorizationService)
        {
            var bug = Bugs.Single(x => x.Id == bugId);
            await bug.MarkAsDone(memberId, authorizationService);
            Update(new BugMarkedAsDone(bugId, Contracts.Issue.Enums.IssueStatus.Done));
        }

        public async System.Threading.Tasks.Task AssignAssigneeToBug(Guid bugId, User.Model.Member assignee, IMembershipService authorizationService)
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
    }
}
