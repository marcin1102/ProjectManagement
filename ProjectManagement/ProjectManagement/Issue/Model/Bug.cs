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
    public class ChildBug : Abstract.Issue
    {
        private ChildBug() : base() { }

        public ChildBug(Guid id, Guid projectId, string title, string description, IssueStatus status, Guid reporterId, Guid? assigneeId, DateTime createdAt, DateTime updatedAt) :
            base(id, projectId, title, description, status, reporterId, assigneeId, createdAt, updatedAt)
        {
        }

        public ChildBug(Guid id, Guid projectId, string title, string description, IssueStatus status, Guid reporterId, Guid? assigneeId, DateTime createdAt, DateTime updatedAt,
            ICollection<Label.Label> labels, ICollection<Comment.Comment> comments) :
            base(id, projectId, title, description, status, reporterId, assigneeId, createdAt, updatedAt)
        {
            Labels = labels;
            Comments = comments;
        }
    }

    public class Bug : Abstract.AggregateIssue
    {
        private Bug() { }

        public Bug(Guid id, Guid projectId, string title, string description, IssueStatus status, Guid reporterId, Guid? assigneeId, DateTime createdAt, DateTime updatedAt) :
            base(id, projectId, title, description, status, reporterId, assigneeId, createdAt, updatedAt)
        { }

        public Bug(Guid id, Guid projectId, string title, string description, IssueStatus status, Guid reporterId, Guid? assigneeId, DateTime createdAt, DateTime updatedAt,
            ICollection<Label.Label> labels, ICollection<Comment.Comment> comments) :
            base(id, projectId, title, description, status, reporterId, assigneeId, createdAt, updatedAt)
        {
            Labels = labels;
            Comments = comments;
        }

        public override void Created()
        {
            Update(new BugCreated(Id, ProjectId, Title, Description, ReporterId, AssigneeId, Labels.Select(x => x.Id).ToList(), CreatedAt));
        }

        public override void AssignLabels(ICollection<Guid> requestedLabelsIds, ICollection<Label.Label> fetchedLabels)
        {
            base.AssignLabels(requestedLabelsIds, fetchedLabels);
            Update(new LabelAssignedToBug(Id, Labels.Select(x => x.Id).ToList()));
        }

        public override void MarkAsInProgress()
        {
            base.MarkAsInProgress();
            Update(new BugMarkedAsInProgress(Id, Status));
        }

        public override async System.Threading.Tasks.Task Comment(Guid memberId, string content, IAuthorizationService authorizationService)
        {
            await base.Comment(memberId, content, authorizationService);
            var comment = Comments.OrderBy(x => x.CreatedAt).Last();
            Update(new BugCommented(Id, comment.Id, comment.Content, comment.MemberId, comment.CreatedAt));
        }

        public override void MarkAsDone()
        {
            base.MarkAsDone();
            Update(new BugMarkedAsDone(Id, Status));
        }
        public override async System.Threading.Tasks.Task AssignAssignee(User.Model.User assignee, IAuthorizationService authorizationService)
        {
            await base.AssignAssignee(assignee, authorizationService);
            Update(new AssigneeAssignedToBug(Id, AssigneeId.Value));
        }

        public override async System.Threading.Tasks.Task AssignToSprint(Guid sprintId, ISprintSearcher sprintSearcher)
        {
            await base.AssignToSprint(sprintId, sprintSearcher);
            Update(new BugAssignedToSprint(Id, SprintId.Value));
        }

        public void TasksBugChangedToBug()
        {
            //TODO: Publish events about change from child bug to bug
            //Update(new TasksBugChangedToBug())
        }
    }    
}
