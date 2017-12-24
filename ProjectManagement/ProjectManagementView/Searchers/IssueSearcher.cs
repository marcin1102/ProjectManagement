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
    }

    public class IssueSearcher : IIssueSearcher
    {
        private readonly ProjectManagementViewContext db;

        public IssueSearcher(ProjectManagementViewContext db)
        {
            this.db = db;
        }

        public Task<List<Issue>> GetProjectIssues(Guid projectId)
        {
            return db.Issues.Include(x => x.Assignee).Include(x => x.Reporter).Where(x => x.ProjectId == projectId).ToListAsync();
        }
    }
}
