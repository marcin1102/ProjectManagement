using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Storage.EF;

namespace ProjectManagement.IssueSubtasks
{
    public class IssueSubtask : IEntity
    {
        public IssueSubtask(Guid projectId, Guid issueId, Guid subtaskId)
        {
            ProjectId = projectId;
            IssueId = issueId;
            SubtaskId = subtaskId;
        }

        private IssueSubtask() { }

        public Guid Id { get; private set; }
        public Guid ProjectId { get; private set; }
        public Guid IssueId { get; private set; }
        public Guid SubtaskId { get; private set; }
    }
}
