using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Exceptions;
using Infrastructure.Message.EventDispatcher;
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

        protected override ICollection<IssueLabel> CreateInstancesOfIssueLabel(Guid issueId, IEnumerable<Guid> labelsIds)
        {
            return labelsIds.Select(labelId => new IssueLabel(issueId, labelId)).ToList();
        }

        public async Task<Model.Task> GetWithBugsAndSubtasksAsync(Guid id)
        {
            var task = await Query.Include(x => x.Bugs).Include(x => x.Subtasks).SingleOrDefaultAsync(x => x.Id == id);
            return task;
        }

        public async Task<Model.Task> GetWithBugsAsync(Guid id)
        {
            var task = await Query.Include(x => x.Bugs).SingleOrDefaultAsync(x => x.Id == id);
            return task;
        }

        public async Task<Model.Task> GetWithBugsAndCommentsAsync(Guid id)
        {
            var task = await Query.Include(x => x.Bugs).SingleOrDefaultAsync(x => x.Id == id);
            foreach (var bug in task.Bugs)
            {
                dbContext.Entry(bug)
                    .Collection(x => x.Comments)
                    .Load();
            }
            return task;
        }

        public async Task<Model.Task> GetWithBugsAndLabelsAsync(Guid id)
        {
            var task = await Query.Include(x => x.Bugs).SingleOrDefaultAsync(x => x.Id == id);
            foreach (var bug in task.Bugs)
            {
                var labelsIds = await issueLabelsQuery.Where(x => x.IssueId == bug.Id).Select(x => x.LabelId).ToListAsync();
                bug.Labels = await labelsQuery.Where(x => labelsIds.Contains(x.Id)).ToListAsync();
            }
            return task;
        }

        public async Task<Model.Task> GetWithSubtasksAsync(Guid id)
        {
            var task = await Query.Include(x => x.Subtasks).SingleOrDefaultAsync(x => x.Id == id);
            return task;
        }

        public async Task<Model.Task> GetWithSubtasksAndCommentsAsync(Guid id)
        {
            var task = await Query.Include(x => x.Subtasks).SingleOrDefaultAsync(x => x.Id == id);
            foreach (var subtask in task.Subtasks)
            {
                dbContext.Entry(subtask)
                    .Collection(x => x.Comments)
                    .Load();
            }
            return task;
        }

        public async Task<Model.Task> GetWithSubtasksAndLabelsAsync(Guid id)
        {
            var task = await Query.Include(x => x.Subtasks).SingleOrDefaultAsync(x => x.Id == id);
            foreach (var subtask in task.Subtasks)
            {
                var labelsIds = await issueLabelsQuery.Where(x => x.IssueId == subtask.Id).Select(x => x.LabelId).ToListAsync();
                subtask.Labels = await labelsQuery.Where(x => labelsIds.Contains(x.Id)).ToListAsync();
            }
            return task;
        }
    }
}
