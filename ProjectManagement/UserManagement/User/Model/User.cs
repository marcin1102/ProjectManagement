using System;
using Infrastructure.Storage;
using UserManagement.Contracts.User.Enums;
using UserManagement.Contracts.User.Events;

namespace UserManagement.User.Model
{
    public class User : AggregateRoot
    {
        private User()
        {
        }

        public User(Guid id, string firstName, string lastName, string email, Role role) : base(id)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            this.role = role;
            Update(new UserCreated(Id, FirstName, LastName, Email, role));
        }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        private Role role;
        public string Role
            {
                get => role.ToString();
                set {
                    role = (Role) Enum.Parse(typeof(Role), value);
                }
            }

        public void GrantRole(Role role)
        {
            this.role = role;
            Update(new RoleGranted(Id, role));
        }
    }
}
