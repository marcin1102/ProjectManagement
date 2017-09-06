using System;
using Infrastructure.Message;
using UserManagement.Contracts.User.Enums;

namespace UserManagement.Contracts.User.Events
{
    public class UserCreated : IDomainEvent
    {
        public UserCreated(Guid id, string firstName, string lastName, string email, Role role)
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
        public long AggregateVersion { get; set; }
    }
}
