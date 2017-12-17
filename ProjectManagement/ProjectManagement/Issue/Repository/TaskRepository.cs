using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManagement.Infrastructure.Primitives.Exceptions;
using ProjectManagement.Infrastructure.Message.EventDispatcher;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Issue.Model;
using ProjectManagement.Issue.Model.Abstract;

namespace ProjectManagement.Issue.Repository
{
    public class TaskRepository : IssueRepository<Model.Task>
    {
        public TaskRepository(ProjectManagementContext dbContext, IEventManager eventManager) : base(dbContext, eventManager)
        {
        }

        public override async Task<Model.Task> GetAsync(Guid id)
        {
            var task = await Query
                .Include(x => x.Comments)
                .Include(x => x.Bugs)
                .Include(x => x.Subtasks)
                .SingleOrDefaultAsync(x => x.Id == id) ?? throw new EntityDoesNotExist(id, nameof(Model.Task));

            await FetchDependencies(task);
            return task;
        }

        public override async Task<Model.Task> FindAsync(Guid id)
        {
            var task = await Query
                .Include(x => x.Comments)
                .Include(x => x.Bugs)
                .Include(x => x.Subtasks)
                .SingleOrDefaultAsync(x => x.Id == id);

            if(task == null)
                return null;

            await FetchDependencies(task);
            return task;
        }

        private async System.Threading.Tasks.Task FetchDependencies(Model.Task task)
        {
            foreach (var bug in task.Bugs)
            {
                dbContext.Entry(bug)
                    .Collection(x => x.Comments)
                    .Load();

                var labelsIds = await issueLabelsQuery.Where(x => x.IssueId == bug.Id).Select(x => x.LabelId).ToListAsync();
                bug.Labels = await labelsQuery.Where(x => labelsIds.Contains(x.Id)).ToListAsync();
            }

            foreach (var subtask in task.Subtasks)
            {
                dbContext.Entry(subtask)
                    .Collection(x => x.Comments)
                    .Load();

                var labelsIds = await issueLabelsQuery.Where(x => x.IssueId == subtask.Id).Select(x => x.LabelId).ToListAsync();
                subtask.Labels = await labelsQuery.Where(x => labelsIds.Contains(x.Id)).ToListAsync();
            }
        }
    }
}
