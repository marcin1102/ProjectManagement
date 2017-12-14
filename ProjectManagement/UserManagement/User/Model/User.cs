using System;
using Infrastructure.Storage;
using UserManagement.Contracts.User.Enums;
using UserManagement.Contracts.User.Events;
using UserManagement.Hashing;
using UserManagement.Contracts.User.Exceptions;
using UserManagement.Contracts.User.Commands;
using UserManagement.Authentication;
using System.Threading.Tasks;

namespace UserManagement.User.Model
{
    public class User : AggregateRoot
    {
        private User()
        {
        }

        public User(Guid id, string firstName, string lastName, string email, string password, Role role) : base(id)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            this.Role = role;
            Password = password;
        }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public Role Role { get; private set; }

        public override void Created()
        {
            Update(new UserCreated(Id, FirstName, LastName, Email, Role));
        }

        public void GrantRole(Role role)
        {
            this.Role = role;
            Update(new RoleGranted(Id, role));
        }

        public async Task Login(Login loginCommand, IHashingService hashingService, ITokenFactory tokenFactory, AuthTokenStore tokenStore)
        {
            if (!hashingService.DoPasswordsMatch(loginCommand.Password, Password))
                throw new LoginFailed("UserManagement", "Email or password do not match. Login failed");

            var token = await tokenFactory.Create(Id);
            tokenStore.AddToken(token);
            loginCommand.GeneratedToken = token.Value;
        }
    }
}
