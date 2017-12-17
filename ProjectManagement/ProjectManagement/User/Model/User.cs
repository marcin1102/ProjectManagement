using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManagement.Infrastructure.Storage.EF;
using UserManagement.Contracts.User.Enums;

namespace ProjectManagement.User.Model
{
    public class User : IEntity
    {
        private User() { }

        public User(Guid id, string firstName, string lastName, string email, Role role, long aggregateVersion)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Role = role;
            AggregateVersion = aggregateVersion;
        }

        public Guid Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public Role Role { get; private set; }

        public long AggregateVersion { get; set; }

        public void GrantRole(Role role, long version)
        {
            Role = role;
            AggregateVersion = version;
        }
    }
}
