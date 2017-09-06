using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Storage.EF;
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
            this.role = role;
            AggregateVersion = aggregateVersion;
        }

        public Guid Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        private Role role;
        public string Role {
            get => role.ToString();
            set {
                role = (Role)Enum.Parse(typeof(Role), value);
            }
        }
        public long AggregateVersion { get; set; }

        public void GrantRole(Role role, long version)
        {
            this.role = role;
            AggregateVersion = version;
        }
    }
}
