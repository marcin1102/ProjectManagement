using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Message;

namespace ProjectManagement.Contracts.Issue.Events
{
    public interface ILabelAssignedToIssue
    {
        Guid IssueId { get; }
        ICollection<Guid> LabelsIds { get; }
    }
}
