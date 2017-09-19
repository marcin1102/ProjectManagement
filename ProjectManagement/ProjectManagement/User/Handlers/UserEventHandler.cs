using System;
using System.Threading.Tasks;
using Infrastructure.Exceptions;
using Infrastructure.Message.Handlers;
using ProjectManagement.User.Repository;
using UserManagement.Contracts.User.Events;

namespace ProjectManagement.User.Handlers
{
    public class UserEventHandler :
        IAsyncEventHandler<UserCreated>,
        IAsyncEventHandler<RoleGranted>
    {
        private readonly UserRepository repository;

        public UserEventHandler(UserRepository repository)
        {
            this.repository = repository;
        }

        public async Task HandleAsync(UserCreated @event)
        {
            await repository.AddAsync(new Model.User(@event.Id, @event.FirstName, @event.LastName, @event.Email, @event.Role, @event.AggregateVersion));
        }

        public async Task HandleAsync(RoleGranted @event)
        {
            var user = await repository.FindAsync(@event.UserId);
            user.GrantRole(@event.Role, @event.AggregateVersion);
            await repository.Update(user);
        }
    }
}
