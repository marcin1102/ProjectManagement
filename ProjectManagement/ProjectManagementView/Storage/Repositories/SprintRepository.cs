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
    public class SprintRepository : Repository<Sprint>
    {
        public SprintRepository(ProjectManagementViewContext dbContext) : base(dbContext)
        {
        }

        public override async Task<Sprint> GetAsync(Guid id)
        {
            var project = await Query
                .Include(x => x.Bugs).Include(x => x.Tasks).Include(x => x.Subtasks)
                .Include(x => x.Nfrs).SingleOrDefaultAsync(x => x.Id == id);
            if (project == null)
                throw new EntityDoesNotExist(id, nameof(Sprint));
            return project;
        }

        public override Task<Sprint> FindAsync(Guid id)
        {
            return Query
                .Include(x => x.Bugs).Include(x => x.Tasks).Include(x => x.Subtasks)
                .Include(x => x.Nfrs).SingleOrDefaultAsync(x => x.Id == id);
        }
    }
}
