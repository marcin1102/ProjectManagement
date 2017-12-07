using System;
using Infrastructure.Storage;
using UserManagement.Contracts.User.Enums;
using UserManagement.Contracts.User.Events;
using UserManagement.Hashing;
using UserManagement.Authorization;
using System.Linq;
using Infrastructure.Exceptions;
using UserManagement.Contracts.User.Exceptions;
using UserManagement.Contracts.User.Commands;

namespace UserManagement.User.Model
{
    public class User : AggregateRoot
    {
        private User()
        {
        }

        public User(Guid id, string firstName, string lastName, string email, string password, Role role, IHashingService hashingService) : base(id)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            this.role = role;
            Password = hashingService.GeneratePasswordHash(password);
            Update(new UserCreated(Id, FirstName, LastName, Email, role));
        }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
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

        public void Login(Login loginCommand, IHashingService hashingService)
        {
            if (!hashingService.DoPasswordsMatch(loginCommand.Password, Password))
                throw new LoginFailed("UserManagement", "Email or password do not match. Login failed");

            var tokenValue = Guid.NewGuid().ToString();
            AuthTokenStore.AddToken(tokenValue, Id);
            loginCommand.GeneratedToken = tokenValue;
        }
    }
}
