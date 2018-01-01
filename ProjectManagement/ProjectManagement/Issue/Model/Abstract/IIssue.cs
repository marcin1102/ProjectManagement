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
        System.Threading.Tasks.Task Comment(Guid memberId, string content, IMembershipService authorizationService);
        System.Threading.Tasks.Task MarkAsInProgress(Guid memberId, IMembershipService authorizationService);
        System.Threading.Tasks.Task MarkAsDone(Guid memberId, IMembershipService authorizationService);
        System.Threading.Tasks.Task AssignAssignee(User.Model.Member Assignee, IMembershipService authorizationService);
        System.Threading.Tasks.Task AssignToSprint(Guid sprintId, ISprintSearcher sprintSearcher);
    }
}
