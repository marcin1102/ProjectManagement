using Microsoft.EntityFrameworkCore;
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

        public Task<List<Issue>> GetProjectIssues(Guid projectId)
        {
            return db.Issues.Include(x => x.Assignee).Include(x => x.Reporter).Where(x => x.ProjectId == projectId).ToListAsync();
        }
    }
}
