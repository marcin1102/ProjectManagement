using System.Threading.Tasks;
using ProjectManagement.Infrastructure.Primitives.Exceptions;
using ProjectManagement.Infrastructure.Message.Handlers;
using Microsoft.EntityFrameworkCore;
using UserManagement.Contracts.User.Events;

namespace ProjectManagementView.Storage.Handlers
{
    public class UserEventHandler :
        IAsyncEventHandler<UserCreated>,
        IAsyncEventHandler<RoleGranted>
    {
        private readonly ProjectManagementViewContext db;

        public UserEventHandler(ProjectManagementViewContext db)
        {
            this.db = db;
        }

        public async Task HandleAsync(UserCreated @event)
        {
            var user = new Models.User(@event.Id)
            {
                FirstName = @event.FirstName,
                LastName = @event.LastName,
                Role = @event.Role,
                Email = @event.Email,
                Version = @event.AggregateVersion
            };

            await db.Users.AddAsync(user);
            await db.SaveChangesAsync();
        }

        public async Task HandleAsync(RoleGranted @event)
        {
            var user = await db.Users.SingleOrDefaultAsync(x => x.Id == @event.UserId);
            if (user == null)
                throw new EntityDoesNotExist(@event.UserId, nameof(Models.User));

            user.Role = @event.Role;
            await db.SaveChangesAsync();
        }
    }
}
