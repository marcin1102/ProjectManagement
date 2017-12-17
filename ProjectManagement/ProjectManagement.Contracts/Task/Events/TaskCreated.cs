using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManagement.Infrastructure.Primitives.Message;
using ProjectManagement.Contracts.Issue.Events;

namespace ProjectManagement.Contracts.Task.Events
{
    public class TaskCreated : IIssueCreated, IDomainEvent
    {
        public TaskCreated(Guid id, Guid projectId, string title, string description, Guid reporterId, Guid? assigneeId, ICollection<Guid> labelsIds, DateTime createdAt)
        {
            Id = id;
            ProjectId = projectId;
            Title = title;
            Description = description;
            ReporterId = reporterId;
            AssigneeId = assigneeId;
            LabelsIds = labelsIds;
            CreatedAt = createdAt;
        }

        public Guid Id { get; private set; }
        public Guid ProjectId { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public Guid ReporterId { get; private set; }
        public Guid? AssigneeId { get; private set; }
        public ICollection<Guid> LabelsIds { get; private set; }
        public DateTime CreatedAt { get; set; }
        public long AggregateVersion { get; set; }
    }
}
