using ProjectManagement.Contracts.Issue.Enums;
using System;
using System.Collections.Generic;
using ProjectManagement.Services;
using ProjectManagement.Sprint.Searchers;

namespace ProjectManagement.Issue.Model.Abstract
{
    public interface IIssue
    {
        Guid ProjectId { get; }
        string Title { get; }
        string Description { get; }
        IssueStatus Status { get; }
        Guid ReporterId { get; }
        Guid? AssigneeId { get; }
        DateTime CreatedAt { get; }
        DateTime UpdatedAt { get; }
        Guid? SprintId { get; }
        ICollection<Comment.Comment> Comments { get; }
        ICollection<Label.Label> Labels { get; set; }

        void AssignLabels(ICollection<Guid> requestedLabelsIds, ICollection<Label.Label> fetchedLabels);
        void Comment(Guid memberId, string content, IAuthorizationService authorizationService);
        void MarkAsInProgress();
        void AssignAssignee(User.Model.User Assignee, IAuthorizationService authorizationService);
        void AssignToSprint(Guid sprintId, ISprintSearcher sprintSearcher);
    }

    public class IssueLabel
    {
        private IssueLabel() { }
        public IssueLabel(Guid issueId, Guid labelId)
        {
            IssueId = issueId;
            LabelId = labelId;
        }

        public Guid IssueId { get; private set; }
        public Guid LabelId { get; private set; }
    }
}
