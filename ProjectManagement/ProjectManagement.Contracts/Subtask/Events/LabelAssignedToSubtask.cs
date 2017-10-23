using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Message;
using ProjectManagement.Contracts.Issue.Events;

namespace ProjectManagement.Contracts.Subtask.Events
{
    public class LabelAssignedToSubtask : ILabelAssignedToIssue, IDomainEvent
    {
        public LabelAssignedToSubtask(Guid bugId, ICollection<Guid> labelsIds)
        {
            IssueId = bugId;
            LabelsIds = labelsIds;
        }

        public Guid IssueId { get; private set; }
        public ICollection<Guid> LabelsIds { get; private set; }
        public long AggregateVersion { get; set; }
    }
}
