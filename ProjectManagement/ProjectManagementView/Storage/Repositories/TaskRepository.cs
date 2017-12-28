using Microsoft.EntityFrameworkCore;
using ProjectManagement.Infrastructure.Primitives.Exceptions;
using ProjectManagement.Infrastructure.Storage.EF.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementView.Storage.Repositories
{
    public class TaskRepository : Repository<Models.Task>
    {
        public TaskRepository(ProjectManagementViewContext dbContext) : base(dbContext)
        {
        }

        public override async Task<Models.Task> GetAsync(Guid id)
        {
            var task = await Query
                .Include(x => x.Bugs).Include(x => x.Subtasks)
                .Include(x => x.Assignee).Include(x => x.Reporter)
                .SingleOrDefaultAsync(x => x.Id == id);
            if (task == null)
                throw new EntityDoesNotExist(id, nameof(Models.Task));
            return task;
        }

        public override Task<Models.Task> FindAsync(Guid id)
        {
            return Query
                .Include(x => x.Bugs).Include(x => x.Subtasks)
                .Include(x => x.Assignee).Include(x => x.Reporter)
                .SingleOrDefaultAsync(x => x.Id == id);
        }
    }
}
