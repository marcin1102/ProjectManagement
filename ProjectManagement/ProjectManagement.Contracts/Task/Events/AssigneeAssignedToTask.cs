using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManagement.Infrastructure.Primitives.Message;
using ProjectManagement.Contracts.Issue.Events;

namespace ProjectManagement.Contracts.Task.Events
{
    public class AssigneeAssignedToTask : IAssigneeAssigned, IDomainEvent
    {
        public AssigneeAssignedToTask(Guid issueId, Guid assigneedId)
        {
            IssueId = issueId;
            AssigneeId = assigneedId;
        }

        public Guid IssueId { get; private set; }
        public Guid AssigneeId { get; private set; }
        public long AggregateVersion { get; set; }
    }
}
