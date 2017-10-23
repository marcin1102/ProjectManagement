using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Message;
using ProjectManagement.Contracts.Issue.Events;

namespace ProjectManagement.Contracts.Nfr.Events
{
    public class LabelAssignedToNfr : ILabelAssignedToIssue, IDomainEvent
    {
        public LabelAssignedToNfr(Guid nfrId, ICollection<Guid> labelsIds)
        {
            IssueId = nfrId;
            LabelsIds = labelsIds;
        }

        public Guid IssueId { get; private set; }
        public ICollection<Guid> LabelsIds { get; private set; }
        public long AggregateVersion { get; set; }
    }
}
