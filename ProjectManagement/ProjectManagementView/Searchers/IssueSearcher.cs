using Microsoft.EntityFrameworkCore;
using ProjectManagement.Infrastructure.Primitives.Exceptions;
using ProjectManagementView.Storage.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementView.Searchers
{
    public interface IIssueSearcher
    {
        Task<List<Issue>> GetProjectIssues(Guid projectId);
        Task<List<Issue>> GetIssuesRelatedToTask(Guid taskId);
        Task<List<Issue>> GetIssuesRelatedToNfr(Guid nfrId);
        Task<Issue> GetParentIssueToSubtask(Guid id);
        Task<Issue> GetParentIssueToBug(Guid id);
    }

    public class IssueSearcher : IIssueSearcher
    {
        private readonly ProjectManagementViewContext db;

        public IssueSearcher(ProjectManagementViewContext db)
        {
            this.db = db;
        }

        public async Task<List<Issue>> GetIssuesRelatedToNfr(Guid nfrId)
        {
            var nfr = await db.Nfrs.Include(x => x.Bugs).SingleAsync(x => x.Id == nfrId);
            return nfr.Bugs.Cast<Issue>().ToList();
        }

        public async Task<List<Issue>> GetIssuesRelatedToTask(Guid taskId)
        {
            var task = await db.Tasks.Include(x => x.Bugs).Include(x => x.Subtasks).SingleAsync(x => x.Id == taskId);
            var relatedTasks = task.Bugs.ToList().Cast<Issue>().Union(task.Subtasks).ToList();
            return relatedTasks;
        }

        public async Task<Issue> GetParentIssueToBug(Guid bugId)
        {
            var task = await db.Tasks.Include(x => x.Bugs).SingleOrDefaultAsync(x => x.Bugs.Any(y => y.Id == bugId));
            if (task != null)
                return task;

            var nfr = await db.Nfrs.Include(x => x.Bugs).SingleOrDefaultAsync(x => x.Bugs.Any(y => y.Id == bugId));
            return nfr;
        }

        public async Task<Issue> GetParentIssueToSubtask(Guid subtaskId)
        {
            var task = await db.Tasks.Include(x => x.Subtasks).SingleOrDefaultAsync(x => x.Subtasks.Any(y => y.Id == subtaskId));
            if (task == null)
                throw new EntityDoesNotExist($"Parent Issue to Subtask with id {subtaskId} does not exist");
            return task;
        }

        public Task<List<Issue>> GetProjectIssues(Guid projectId)
        {
            return db.Issues.Include(x => x.Assignee).Include(x => x.Reporter).Where(x => x.ProjectId == projectId).ToListAsync();
        }
    }
}
