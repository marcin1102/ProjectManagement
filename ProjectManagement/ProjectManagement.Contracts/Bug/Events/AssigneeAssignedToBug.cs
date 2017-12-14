using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Message;
using ProjectManagement.Contracts.Issue.Events;

namespace ProjectManagement.Contracts.Bug.Events
{
    public class AssigneeAssignedToBug : IAssigneeAssigned, IDomainEvent
    {
        public AssigneeAssignedToBug(Guid bugId, Guid assigneedId)
        {
            IssueId = bugId;
            AssigneeId = assigneedId;
        }

        public Guid IssueId { get; private set; }
        public Guid AssigneeId { get; private set; }
        public long AggregateVersion { get; set; }
    }
}
