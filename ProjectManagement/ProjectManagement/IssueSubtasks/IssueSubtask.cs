using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Storage.EF;

namespace ProjectManagement.IssueSubtasks
{
    public class IssueSubtask : IEntity
    {
        private IssueSubtask() { }

        public IssueSubtask(Guid id, Guid issueId, Guid subtaskId)
        {
            Id = id;
            IssueId = issueId;
            SubtaskId = subtaskId;
        }

        public Guid Id { get; private set; }
        public Guid IssueId { get; private set; }
        public Guid SubtaskId { get; private set; }
    }
}
