using ProjectManagement.Infrastructure.Primitives.Message;
using System;
using System.Collections.Generic;

namespace ProjectManagement.Contracts.Bug.Events
{
    public class BugAddedToNfr : IDomainEvent
    {
        public BugAddedToNfr(Guid bugId, Guid nfrId, Guid projectId, string title, string description, Guid reporterId, Guid? assigneeId, ICollection<Guid> labelsIds, DateTime createdAt)
        {
            NfrId = nfrId;
            BugId = bugId;
            ProjectId = projectId;
            Title = title;
            Description = description;
            ReporterId = reporterId;
            AssigneeId = assigneeId;
            LabelsIds = labelsIds;
            CreatedAt = createdAt;
        }

        public Guid BugId { get; private set; }
        public Guid NfrId { get; private set; }
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
