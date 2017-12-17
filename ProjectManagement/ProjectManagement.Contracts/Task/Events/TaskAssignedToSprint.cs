using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManagement.Infrastructure.Primitives.Message;
using ProjectManagement.Contracts.Issue.Events;

namespace ProjectManagement.Contracts.Task.Events
{
    public class TaskAssignedToSprint : IIssueAssignedToSprint, IDomainEvent
    {
        public TaskAssignedToSprint(Guid issueId, Guid sprintId)
        {
            IssueId = issueId;
            SprintId = sprintId;
        }

        public Guid IssueId { get; private set; }
        public Guid SprintId { get; private set; }
        public long AggregateVersion { get; set; }
    }
}
