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
    public class NfrRepository : Repository<Models.Nfr>
    {
        public NfrRepository(ProjectManagementViewContext dbContext) : base(dbContext)
        {
        }

        public override async Task<Models.Nfr> GetAsync(Guid id)
        {
            var nfr = await Query
                .Include(x => x.Bugs)
                .Include(x => x.Assignee).Include(x => x.Reporter)
                .SingleOrDefaultAsync(x => x.Id == id);
            if (nfr == null)
                throw new EntityDoesNotExist(id, nameof(Models.Nfr));
            return nfr;
        }

        public override Task<Models.Nfr> FindAsync(Guid id)
        {
            return Query
                .Include(x => x.Bugs)
                .Include(x => x.Assignee).Include(x => x.Reporter)
                .SingleOrDefaultAsync(x => x.Id == id);
        }
    }
}
