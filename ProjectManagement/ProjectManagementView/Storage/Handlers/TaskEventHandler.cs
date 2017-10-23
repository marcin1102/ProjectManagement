using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Exceptions;
using Infrastructure.Message.Handlers;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Contracts.Issue.Enums;
using ProjectManagement.Contracts.Task.Events;

namespace ProjectManagementView.Storage.Handlers
{
    public class TaskEventHandler :
        IAsyncEventHandler<TaskCreated>,
        IAsyncEventHandler<TaskAssignedToSprint>,
        IAsyncEventHandler<AssigneeAssignedToTask>,
        IAsyncEventHandler<LabelAssignedToTask>,
        IAsyncEventHandler<TaskCommented>,
        IAsyncEventHandler<TaskMarkedAsInProgress>,
        IAsyncEventHandler<TaskMarkedAsDone>
    {
        private readonly ProjectManagementViewContext db;

        public TaskEventHandler(ProjectManagementViewContext db)
        {
            this.db = db;
        }

        public async Task HandleAsync(TaskCreated @event)
        {
            var project = await db.Projects.SingleOrDefaultAsync(x => x.Id == @event.ProjectId);
            if (project == null)
                throw new EntityDoesNotExist(@event.ProjectId, nameof(Models.Project));

            var reporter = await db.Users.SingleOrDefaultAsync(x => x.Id == @event.ReporterId);
            var assignee = await db.Users.SingleOrDefaultAsync(x => x.Id == @event.AssigneeId);
            var labels = await db.Labels.Where(x => @event.LabelsIds.Contains(x.Id)).ToListAsync();
            project.Tasks.Add(new Models.Task(@event.Id)
            {
                Title = @event.Title,
                Description = @event.Description,
                Status = IssueStatus.Todo,
                Reporter = reporter,
                Assignee = assignee,
                CreatedAt = @event.CreatedAt,
                Labels = labels
            });
        }

        public Task HandleAsync(TaskAssignedToSprint @event)
        {
            throw new NotImplementedException();
        }

        public Task HandleAsync(AssigneeAssignedToTask @event)
        {
            throw new NotImplementedException();
        }

        public Task HandleAsync(LabelAssignedToTask @event)
        {
            throw new NotImplementedException();
        }

        public Task HandleAsync(TaskCommented @event)
        {
            throw new NotImplementedException();
        }

        public Task HandleAsync(TaskMarkedAsInProgress @event)
        {
            throw new NotImplementedException();
        }

        public Task HandleAsync(TaskMarkedAsDone @event)
        {
            throw new NotImplementedException();
        }
    }
}
