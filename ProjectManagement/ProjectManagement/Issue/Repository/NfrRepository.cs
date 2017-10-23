using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        protected override ICollection<IssueLabel> CreateInstancesOfIssueLabel(Guid issueId, IEnumerable<Guid> labelsIds)
        {
            return labelsIds.Select(labelId => new IssueLabel(issueId, labelId)).ToList();
        }

        public async Task<Nfr> GetWithBugsAsync(Guid id)
        {
            var nfr = await Query.Include(x => x.Bugs).SingleOrDefaultAsync(x => x.Id == id);
            return nfr;
        }

        public async Task<Nfr> GetWithBugsAndCommentsAsync(Guid id)
        {
            var nfr = await Query.Include(x => x.Bugs).SingleOrDefaultAsync(x => x.Id == id);
            foreach (var bug in nfr.Bugs)
            {
                dbContext.Entry(bug)
                    .Collection(x => x.Comments)
                    .Load();
            }
            return nfr;
        }

        public async Task<Nfr> GetWithBugsAndLabelsAsync(Guid id)
        {
            var nfr = await Query.Include(x => x.Bugs).SingleOrDefaultAsync(x => x.Id == id);
            foreach (var bug in nfr.Bugs)
            {
                var labelsIds = await issueLabelsQuery.Where(x => x.IssueId == bug.Id).Select(x => x.LabelId).ToListAsync();
                bug.Labels = await labelsQuery.Where(x => labelsIds.Contains(x.Id)).ToListAsync();
            }
            return nfr;
        }
    }
}
