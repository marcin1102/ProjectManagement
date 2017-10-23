using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Message;
using ProjectManagement.Contracts.Issue.Events;

namespace ProjectManagement.Contracts.Nfr.Events
{
    public class AssigneeAssignedToNfr : IAssigneeAssigned, IDomainEvent
    {
        public AssigneeAssignedToNfr(Guid nfrId, Guid assigneedId)
        {
            IssueId = nfrId;
            AssigneedId = assigneedId;
        }

        public Guid IssueId { get; private set; }
        public Guid AssigneedId { get; private set; }
        public long AggregateVersion { get; set; }
    }
}
