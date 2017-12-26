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
    public class IssueRepository : Repository<Models.Abstract.Issue>
    {
        public IssueRepository(ProjectManagementViewContext dbContext) : base(dbContext)
        {
        }

        public override async Task<Models.Abstract.Issue> GetAsync(Guid id)
        {
            var nfr = await Query
                .Include(x => x.Assignee)
                .Include(x => x.Reporter)
                .SingleOrDefaultAsync(x => x.Id == id);
            if (nfr == null)
                throw new EntityDoesNotExist(id, nameof(Models.Abstract.Issue));
            return nfr;
        }

        public override Task<Models.Abstract.Issue> FindAsync(Guid id)
        {
            return Query
                .Include(x => x.Assignee)
                .Include(x => x.Reporter)
                .SingleOrDefaultAsync(x => x.Id == id);
        }
    }
}
