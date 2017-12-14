using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Message;
using ProjectManagement.Contracts.Issue.Events;

namespace ProjectManagement.Contracts.Subtask.Events
{
    public class AssigneeAssignedToSubtask : IAssigneeAssigned, IDomainEvent
    {
        public AssigneeAssignedToSubtask(Guid subtaskId, Guid assigneedId)
        {
            IssueId = subtaskId;
            AssigneeId = assigneedId;
        }

        public Guid IssueId { get; private set; }
        public Guid AssigneeId { get; private set; }
        public long AggregateVersion { get; set; }
    }
}
