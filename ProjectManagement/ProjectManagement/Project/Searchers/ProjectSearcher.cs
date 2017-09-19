using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Project.Model;

namespace ProjectManagement.Project.Searchers
{
    public interface IProjectSearcher
    {
        Task<List<Model.Project>> GetProjects();
    }

    public class ProjectSearcher : IProjectSearcher
    {
        private readonly ProjectManagementContext db;

        public ProjectSearcher(ProjectManagementContext db)
        {
            this.db = db;
        }

        public Task<List<Model.Project>> GetProjects()
        {
            return db.Projects.ToListAsync();
        }
    }
}
