using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManagement.Infrastructure.Storage.EF;
using UserManagement.Contracts.User.Enums;

namespace ProjectManagement.User.Model
{
    public class Member : IEntity
    {
        private Member() { }

        public Member(Guid id, string firstName, string lastName, string email, Role role)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Role = role;
        }

        public Guid Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public Role Role { get; private set; }

        public void GrantRole(Role role)
        {
            Role = role;
        }
    }
}
