using System;
using System.Threading.Tasks;
using Infrastructure.Exceptions;
using Infrastructure.Message.Handlers;
using UserManagement.Contracts.User.Commands;
using UserManagement.User.Repository;

namespace UserManagement.User.Handlers
{
    public class UserCommandHandler :
        IAsyncCommandHandler<CreateUser>,
        IAsyncCommandHandler<GrantRole>
    {
        private readonly UserRepository repository;

        public UserCommandHandler(UserRepository repository)
        {
            this.repository = repository;
        }

        public Task HandleAsync(CreateUser command)
        {
            command.CreatedId = Guid.NewGuid();
            var user = new Model.User(command.CreatedId, command.FirstName, command.LastName, command.Email, command.Role);
            return repository.AddAsync(user);
        }

        public async Task HandleAsync(GrantRole command)
        {
            var user = await repository.GetAsync(command.UserId);

            user.GrantRole(command.Role);
            await repository.Update(user, command.AggregateVersion);
        }
    }
}
