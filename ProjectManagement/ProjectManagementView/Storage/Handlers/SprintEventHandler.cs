using System.Threading.Tasks;
using Infrastructure.Exceptions;
using Infrastructure.Message.Handlers;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Contracts.Sprint.Events;

namespace ProjectManagementView.Storage.Handlers
{
    public class SprintEventHandler :
        IAsyncEventHandler<SprintCreated>,
        IAsyncEventHandler<SprintStarted>,
        IAsyncEventHandler<SprintFinished>
    {
        private readonly ProjectManagementViewContext db;

        public SprintEventHandler(ProjectManagementViewContext db)
        {
            this.db = db;
        }

        public async Task HandleAsync(SprintCreated @event)
        {
            var project = await db.Projects.SingleOrDefaultAsync(x => x.Id == @event.ProjectId);
            if (project == null)
                throw new EntityDoesNotExist(@event.ProjectId, nameof(Models.Project));

            var sprint = new Models.Sprint(@event.Id)
            {
                Name = @event.Name,
                Start = @event.Start,
                End = @event.End,
                Status = @event.Status,
                Version = @event.AggregateVersion
            };

            project.Sprints.Add(sprint);
            await db.SaveChangesAsync();
        }

        public async Task HandleAsync(SprintStarted @event)
        {
            var sprint = await db.Sprints.SingleOrDefaultAsync(x => x.Id == @event.Id);
            if (sprint == null)
                throw new EntityDoesNotExist(@event.Id, nameof(Models.Sprint));

            sprint.Start = @event.Start.Date;
            sprint.Status = @event.Status;
            sprint.Version = @event.AggregateVersion;
            await db.SaveChangesAsync();
        }

        public async Task HandleAsync(SprintFinished @event)
        {
            var sprint = await db.Sprints.SingleOrDefaultAsync(x => x.Id == @event.Id);
            if (sprint == null)
                throw new EntityDoesNotExist(@event.Id, nameof(Models.Sprint));

            sprint.End = @event.End.Date;
            sprint.Status = @event.Status;
            sprint.Version = @event.AggregateVersion;
            await db.SaveChangesAsync();
        }
    }
}
