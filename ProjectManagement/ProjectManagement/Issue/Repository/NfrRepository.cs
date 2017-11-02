using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Exceptions;
using Infrastructure.Message.EventDispatcher;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Issue.Model;
using ProjectManagement.Issue.Model.Abstract;

namespace ProjectManagement.Issue.Repository
{
    public class NfrRepository : IssueRepository<Nfr>
    {
        public NfrRepository(ProjectManagementContext dbContext, IEventManager eventManager) : base(dbContext, eventManager)
        {
        }


        public override async Task<Nfr> GetAsync(Guid id)
        {
            var nfr = await Query
                .Include(x => x.Comments)
                .Include(x => x.Bugs)
                .SingleOrDefaultAsync(x => x.Id == id) ?? throw new EntityDoesNotExist(id, nameof(Nfr));

            await FetchDependencies(nfr);
            return nfr;
        }

        public override async Task<Nfr> FindAsync(Guid id)
        {
            var nfr = await Query
                .Include(x => x.Comments)
                .Include(x => x.Bugs)
                .SingleOrDefaultAsync(x => x.Id == id);

            if(nfr == null)
                return null;

            await FetchDependencies(nfr);
            return nfr;
        }

        private async System.Threading.Tasks.Task FetchDependencies(Nfr task)
        {
            foreach (var bug in task.Bugs)
            {
                dbContext.Entry(bug)
                    .Collection(x => x.Comments)
                    .Load();

                var labelsIds = await issueLabelsQuery.Where(x => x.IssueId == bug.Id).Select(x => x.LabelId).ToListAsync();
                bug.Labels = await labelsQuery.Where(x => labelsIds.Contains(x.Id)).ToListAsync();
            }
        }
    }
}
