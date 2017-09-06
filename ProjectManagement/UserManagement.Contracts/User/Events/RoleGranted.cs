using System;
using Infrastructure.Message;
using UserManagement.Contracts.User.Enums;

namespace UserManagement.Contracts.User.Events
{
    public class RoleGranted : IDomainEvent
    {
        public RoleGranted(Guid id, Role role)
        {
            Id = id;
            Role = role;
        }

        public Guid Id { get; private set; }
        public Role Role { get; private set; }
        public long AggregateVersion { get; set; }
    }
}
