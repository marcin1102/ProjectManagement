using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManagement.Infrastructure.Primitives.Message;
using ProjectManagement.Contracts.Issue.Enums;

namespace ProjectManagement.Contracts.Issue.Events
{
    public interface IIssueCreated
    {
        Guid Id { get; }
        Guid ProjectId { get; }
        string Title { get; }
        string Description { get; }
        Guid ReporterId { get; }
        Guid? AssigneeId { get; }
        ICollection<Guid> LabelsIds { get; }
        DateTime CreatedAt { get; }
    }
}
