using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManagement.Infrastructure.Primitives.Message;
using ProjectManagement.Contracts.Issue.Events;

namespace ProjectManagement.Contracts.Task.Events
{
    public class LabelAssignedToTask : ILabelAssignedToIssue, IDomainEvent
    {
        public LabelAssignedToTask(Guid issueId, ICollection<Guid> labelsIds)
        {
            IssueId = issueId;
            LabelsIds = labelsIds;
        }

        public Guid IssueId { get; private set; }
        public ICollection<Guid> LabelsIds { get; private set; }
        public long AggregateVersion { get; set; }
    }
}
