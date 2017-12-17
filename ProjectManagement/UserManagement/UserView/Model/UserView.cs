using ProjectManagement.Infrastructure.Storage.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Contracts.User.Enums;

namespace UserManagement.UserView.Model
{
    public class UserView : IEntity
    {
        private UserView() { }

        public UserView(Guid id, string firstName, string lastName, string email, string password, Role role, long version)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
            Role = role;
            Version = version;
        }

        public Guid Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public Role Role { get; private set; }
        public long Version { get; private set; }
    }
}
