using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Message;
using UserManagement.Contracts.User.Enums;

namespace UserManagement.Contracts.User.Commands
{
    public class GrantRole : ICommand
    {
        public GrantRole(Guid userId, Role role, long aggregateVersion)
        {
            UserId = userId;
            Role = role;
            AggregateVersion = aggregateVersion;
        }

        public Guid UserId { get; private set; }
        public Role Role { get; private set; }
        public long AggregateVersion { get; private set; }
    }
}
