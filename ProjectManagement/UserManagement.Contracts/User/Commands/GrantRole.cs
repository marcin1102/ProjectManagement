using System;
using System.Collections.Generic;
using System.Text;
using ProjectManagement.Infrastructure.Primitives.Message;
using UserManagement.Contracts.User.Enums;
using Newtonsoft.Json;

namespace UserManagement.Contracts.User.Commands
{
    public class GrantRole : ICommand
    {
        public GrantRole(Role role, long aggregateVersion)
        {
            Role = role;
            AggregateVersion = aggregateVersion;
        }

        [JsonIgnore]
        public Guid UserId { get; set; }
        public Role Role { get; private set; }
        public long AggregateVersion { get; private set; }
    }
}
