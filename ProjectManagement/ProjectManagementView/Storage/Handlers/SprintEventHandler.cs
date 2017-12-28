using System.Threading.Tasks;
using ProjectManagement.Infrastructure.Primitives.Exceptions;
using ProjectManagement.Infrastructure.Message.Handlers;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Contracts.Sprint.Events;
using ProjectManagementView.Storage.Models.Abstract;
using System.Collections.Generic;
using System.Linq;
using ProjectManagement.Contracts.Issue.Enums;
using ProjectManagementView.Contracts.Issues;
using ProjectManagementView.Helpers;

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
            var project = await db.Projects.Include(x => x.Sprints).SingleOrDefaultAsync(x => x.Id == @event.ProjectId);
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
            var sprint = await db.Sprints.Include(x => x.Tasks).Include(x => x.Subtasks).Include(x => x.Nfrs).Include(x => x.Bugs).SingleOrDefaultAsync(x => x.Id == @event.Id);
            if (sprint == null)
                throw new EntityDoesNotExist(@event.Id, nameof(Models.Sprint));

            sprint.End = @event.End.Date;
            sprint.Status = @event.Status;
            sprint.Version = @event.AggregateVersion;

            var unfinishedIssueIds = @event.UnfinishedIssueIds;
            var issues = await db.Issues.Where(x => unfinishedIssueIds.Any(y => x.Id == y)).ToListAsync();

            var unfinishedIssues = issues.Select(x => new UnfinishedIssue(x.Id, IssueHelpers.GetIssueType(x), x.Title)).ToList();
            sprint.UnfinishedIssues = unfinishedIssues;

            await db.SaveChangesAsync();
        }
    }
}
