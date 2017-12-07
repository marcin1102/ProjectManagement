using System;
using System.Threading.Tasks;
using Infrastructure.Exceptions;
using Infrastructure.Message.Handlers;
using UserManagement.Contracts.User.Commands;
using UserManagement.User.Repository;
using UserManagement.Hashing;
using UserManagement.User.Searchers;
using UserManagement.Contracts.User.Exceptions;

namespace UserManagement.User.Handlers
{
    public class UserCommandHandler :
        IAsyncCommandHandler<CreateUser>,
        IAsyncCommandHandler<GrantRole>,
        IAsyncCommandHandler<Login>
    {
        private readonly UserRepository repository;
        private readonly IHashingService hashingService;
        private readonly IUserSearcher userSearcher;

        public UserCommandHandler(UserRepository repository, IHashingService hashingService, IUserSearcher userSearcher)
        {
            this.repository = repository;
            this.hashingService = hashingService;
            this.userSearcher = userSearcher;
        }

        public Task HandleAsync(CreateUser command)
        {
            command.CreatedId = Guid.NewGuid();
            var user = new Model.User(command.CreatedId, command.FirstName, command.LastName, command.Email, command.Password, command.Role, hashingService);
            return repository.AddAsync(user);
        }

        public async Task HandleAsync(GrantRole command)
        {
            var user = await repository.GetAsync(command.UserId);
            var originalVersion = user.Version;

            user.GrantRole(command.Role);
            await repository.Update(user, originalVersion);
        }

        public async Task HandleAsync(Login command)
        {
            var user = await userSearcher.FindUser(command.Email);
            if (user == null)
                throw new LoginFailed("UserManagement", "Email or password do not match. Login failed");

            user.Login(command, hashingService);
        }
    }
}
