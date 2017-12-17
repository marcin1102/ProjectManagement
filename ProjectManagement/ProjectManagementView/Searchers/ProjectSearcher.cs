using Microsoft.EntityFrameworkCore;
using ProjectManagementView.Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementView.Searchers
{
    public interface IProjectSearcher
    {
        Task<List<Project>> GetProjects();
        Task<List<Project>> GetProjects(Guid userId);
    }

    public class ProjectSearcher : IProjectSearcher
    {
        private readonly ProjectManagementViewContext db;

        public ProjectSearcher(ProjectManagementViewContext db)
        {
            this.db = db;
        }

        public Task<List<Project>> GetProjects()
        {
            return db.Projects.ToListAsync();
        }

        public Task<List<Project>> GetProjects(Guid userId)
        {
            return db.Projects.Include(x => x.Users).Where(x => x.Users.Any(y => y.Id == userId)).ToListAsync();
        }
    }
}
