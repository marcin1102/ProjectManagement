using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManagement.Infrastructure.Primitives.Message;
using ProjectManagement.Contracts.Issue.Enums;
using ProjectManagement.Contracts.Issue.Events;

namespace ProjectManagement.Contracts.Task.Events
{
    public class TaskMarkedAsInProgress : IIssueMarkedAsInProgress, IDomainEvent
    {
        public TaskMarkedAsInProgress(Guid id, IssueStatus status)
        {
            Id = id;
            Status = status;
        }

        public Guid Id { get; private set; }
        public IssueStatus Status { get; private set; }
        public long AggregateVersion { get; set; }
    }
}
