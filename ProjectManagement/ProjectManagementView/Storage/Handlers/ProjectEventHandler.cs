using System.Threading.Tasks;
using Infrastructure.Exceptions;
using Infrastructure.Message.Handlers;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Contracts.Project.Events;

namespace ProjectManagementView.Storage.Handlers
{
    public class ProjectEventHandler :
        IAsyncEventHandler<ProjectCreated>,
        IAsyncEventHandler<UserAssignedToProject>
    {
        private readonly ProjectManagementViewContext db;

        public ProjectEventHandler(ProjectManagementViewContext db)
        {
            this.db = db;
        }

        public async Task HandleAsync(ProjectCreated @event)
        {
            var project = new Models.Project(@event.Id)
            {
                Name = @event.Name,
                Version = @event.AggregateVersion
            };

            await db.Projects.AddAsync(project);
            await db.SaveChangesAsync();
        }

        public async Task HandleAsync(UserAssignedToProject @event)
        {
            var project = await db.Projects.Include(x => x.Users).SingleOrDefaultAsync(x => x.Id == @event.ProjectId);
            if (project == null)
                throw new EntityDoesNotExist(@event.ProjectId, nameof(Models.Project));

            var user = await db.Users.SingleOrDefaultAsync(x => x.Id == @event.UserId);
            if (user == null)
                throw new EntityDoesNotExist(@event.UserId, nameof(Models.User));

            project.Users.Add(user);
            await db.SaveChangesAsync();
        }
    }
}
