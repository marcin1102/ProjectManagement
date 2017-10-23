using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Project.Model;

namespace ProjectManagement.Project.Searchers
{
    public interface IProjectSearcher
    {
        Task<List<Model.Project>> GetProjects();
        Task<bool> IsUserProjectMember(Guid projectId, Guid userId);
        Task<bool> DoesProjectExist(Guid id);
    }

    public class ProjectSearcher : IProjectSearcher
    {
        private readonly ProjectManagementContext db;

        public ProjectSearcher(ProjectManagementContext db)
        {
            this.db = db;
        }

        public Task<bool> DoesProjectExist(Guid id)
        {
            return db.Projects.AnyAsync(x => x.Id == id);
        }

        public Task<List<Model.Project>> GetProjects()
        {
            return db.Projects.ToListAsync();
        }

        public async Task<bool> IsUserProjectMember(Guid projectId, Guid userId)
        {
            var project = await db.Projects.Where(x => x.Id == projectId).SingleOrDefaultAsync() ?? throw new EntityDoesNotExist(projectId, nameof(Model.Project));
            return project.Members.Any(memberId => memberId == userId);
        }
    }
}
