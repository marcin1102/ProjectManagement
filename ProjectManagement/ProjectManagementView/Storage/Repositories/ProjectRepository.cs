using Microsoft.EntityFrameworkCore;
using ProjectManagement.Infrastructure.Primitives.Exceptions;
using ProjectManagement.Infrastructure.Storage.EF.Repository;
using ProjectManagementView.Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementView.Storage.Repositories
{
    public class ProjectRepository : Repository<Project>
    {
        public ProjectRepository(ProjectManagementViewContext dbContext) : base(dbContext)
        {
        }

        public override async Task<Project> GetAsync(Guid id)
        {
            var project = await Query
                .Include(x => x.Bugs).Include(x => x.Tasks).Include(x => x.Subtasks)
                .Include(x => x.Nfrs).Include(x => x.Labels).Include(x => x.Sprints)
                .Include(x => x.Users).SingleOrDefaultAsync(x => x.Id == id);
            if (project == null)
                throw new EntityDoesNotExist(id, nameof(Project));
            return project;
        }

        public override Task<Project> FindAsync(Guid id)
        {
            return Query
                .Include(x => x.Bugs).Include(x => x.Tasks).Include(x => x.Subtasks)
                .Include(x => x.Nfrs).Include(x => x.Labels).Include(x => x.Sprints)
                .Include(x => x.Users).SingleOrDefaultAsync(x => x.Id == id);
        }
    }
}
