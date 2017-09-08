using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Message;

namespace ProjectManagement.Contracts.Project.Events
{
    public class UserAssignedToProject : IDomainEvent
    {
        public UserAssignedToProject(Guid id, Guid userId)
        {
            Id = id;
            UserId = userId;
        }

        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public long AggregateVersion { get; set; }
    }
}
