using System;
using Infrastructure.Message;
using ProjectManagement.Contracts.Issue.Events;

namespace ProjectManagement.Contracts.Bug.Events
{
    public class BugAssignedToSprint : IIssueAssignedToSprint, IDomainEvent
    {
        public BugAssignedToSprint(Guid bugId, Guid sprintId)
        {
            IssueId = bugId;
            SprintId = sprintId;
        }

        public Guid IssueId { get; private set; }
        public Guid SprintId { get; private set; }
        public long AggregateVersion { get; set; }
    }
}
