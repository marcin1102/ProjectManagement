using System;
using Infrastructure.Message;
using ProjectManagement.Contracts.Issue.Events;

namespace ProjectManagement.Contracts.Subtask.Events
{
    public class SubtaskAssignedToSprint : IIssueAssignedToSprint, IDomainEvent
    {
        public SubtaskAssignedToSprint(Guid subtaskId, Guid sprintId)
        {
            IssueId = subtaskId;
            SprintId = sprintId;
        }

        public Guid IssueId { get; private set; }
        public Guid SprintId { get; private set; }
        public long AggregateVersion { get; set; }
    }
}
