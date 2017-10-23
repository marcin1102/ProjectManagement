using System;
using Infrastructure.Message;
using ProjectManagement.Contracts.Issue.Events;

namespace ProjectManagement.Contracts.Nfr.Events
{
    public class NfrAssignedToSprint : IIssueAssignedToSprint, IDomainEvent
    {
        public NfrAssignedToSprint(Guid nfrId, Guid sprintId)
        {
            IssueId = nfrId;
            SprintId = sprintId;
        }

        public Guid IssueId { get; private set; }
        public Guid SprintId { get; private set; }
        public long AggregateVersion { get; set; }
    }
}
