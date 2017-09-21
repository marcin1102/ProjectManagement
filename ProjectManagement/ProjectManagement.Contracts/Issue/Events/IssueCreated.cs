using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Message;
using ProjectManagement.Contracts.Issue.Enums;

namespace ProjectManagement.Contracts.Issue.Events
{
    public class IssueCreated : IDomainEvent
    {
        public IssueCreated(Guid id, Guid projectId, string title, string description, IssueType type, Guid reporterId, Guid? assigneeId, ICollection<Guid> labelsIds, ICollection<Guid> subtasksIds)
        {
            Id = id;
            ProjectId = projectId;
            Title = title;
            Description = description;
            Type = type;
            ReporterId = reporterId;
            AssigneeId = assigneeId;
            LabelsIds = labelsIds;
            SubtasksIds = subtasksIds;
        }

        public Guid Id { get; private set; }
        public Guid ProjectId { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public IssueType Type { get; private set; }
        public Guid ReporterId { get; private set; }
        public Guid? AssigneeId { get; private set; }
        public ICollection<Guid> LabelsIds { get; private set; }
        public ICollection<Guid> SubtasksIds { get; private set; }
        public long AggregateVersion { get; set; }
    }
}
