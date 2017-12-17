using System;
using System.Collections.Generic;
using ProjectManagement.Infrastructure.Primitives.Message;

namespace ProjectManagement.Contracts.Subtask.Events
{
    public class SubtaskAddedToTask : IDomainEvent
    {
        public SubtaskAddedToTask(Guid subtaskId, Guid taskId, Guid projectId, string title, string description, Guid reporterId, Guid? assigneeId, ICollection<Guid> labelsIds, DateTime createdAt)
        {
            TaskId = taskId;
            SubtaskId = subtaskId;
            ProjectId = projectId;
            Title = title;
            Description = description;
            ReporterId = reporterId;
            AssigneeId = assigneeId;
            LabelsIds = labelsIds;
            CreatedAt = createdAt;
        }

        public Guid SubtaskId { get; private set; }
        public Guid TaskId { get; private set; }
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
