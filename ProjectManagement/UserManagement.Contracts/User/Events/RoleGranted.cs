using System;
using Infrastructure.Message;
using UserManagement.Contracts.User.Enums;

namespace UserManagement.Contracts.User.Events
{
    public class RoleGranted : IDomainEvent
    {
        public RoleGranted(Guid userId, Role role)
        {
            UserId = userId;
            Role = role;
        }

        public Guid UserId { get; private set; }
        public Role Role { get; private set; }
        public long AggregateVersion { get; set; }
    }
}
