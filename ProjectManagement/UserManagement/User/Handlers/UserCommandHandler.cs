using System;
using System.Threading.Tasks;
using ProjectManagement.Infrastructure.Primitives.Exceptions;
using ProjectManagement.Infrastructure.Message.Handlers;
using UserManagement.Contracts.User.Commands;
using UserManagement.User.Repository;
using UserManagement.Hashing;
using UserManagement.User.Searchers;
using UserManagement.Contracts.User.Exceptions;
using UserManagement.Authentication;
using UserManagement.User.Services;

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
        private readonly ITokenFactory tokenFactory;
        private readonly AuthTokenStore authTokenStore;
        private readonly IUserFactory userFactory;

        public UserCommandHandler(UserRepository repository, IHashingService hashingService, IUserSearcher userSearcher, ITokenFactory tokenFactory, AuthTokenStore authTokenStore, IUserFactory userFactory)
        {
            this.repository = repository;
            this.hashingService = hashingService;
            this.userSearcher = userSearcher;
            this.tokenFactory = tokenFactory;
            this.authTokenStore = authTokenStore;
            this.userFactory = userFactory;
        }

        public Task HandleAsync(CreateUser command)
        {
            var user = userFactory.Create(command.FirstName, command.LastName, command.Email, command.Password, command.Role);
            command.CreatedId = user.Id;
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

            await user.Login(command, hashingService, tokenFactory, authTokenStore);
        }
    }
}
