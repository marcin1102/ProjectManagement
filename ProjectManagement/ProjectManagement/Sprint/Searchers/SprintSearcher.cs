using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using ProjectManagement.Infrastructure.Primitives.Exceptions;
using System.Collections.Generic;
using ProjectManagement.Sprint.Model;
using System.Linq;

namespace ProjectManagement.Sprint.Searchers
{
    public interface ISprintSearcher
    {
        Task CheckIfSprintExistsInProjectScope(Guid sprintId, Guid projectId);
        Task<List<Model.Sprint>> GetSprints(Guid projectId);
    }

    public class SprintSearcher : ISprintSearcher
    {
        private readonly ProjectManagementContext db;

        public SprintSearcher(ProjectManagementContext db)
        {
            this.db = db;
        }

        public async Task CheckIfSprintExistsInProjectScope(Guid sprintId, Guid projectId)
        {
            var doesExist = await db.Sprints.AnyAsync(x => x.Id == sprintId && x.ProjectId == projectId);
            if (!doesExist)
                throw new EntityDoesNotExistsInScope(sprintId, nameof(Sprint.Model.Sprint), nameof(Project.Model.Project), projectId);
        }

        public Task<List<Model.Sprint>> GetSprints(Guid projectId)
        {
            return db.Sprints.Where(x => x.ProjectId == projectId).ToListAsync();
        }
    }
}
