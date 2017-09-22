using System;
using System.Threading.Tasks;
using Infrastructure.Exceptions;
using Infrastructure.Message.EventDispatcher;
using Infrastructure.Storage.EF.Repository;
using Microsoft.EntityFrameworkCore;

namespace ProjectManagement.Issue.Repository
{
    public class IssueRepository : AggregateRepository<Model.Issue>
    {
        public IssueRepository(ProjectManagementContext dbContext, IEventManager eventManager) : base(dbContext, eventManager)
        {
        }

        public override Task AddAsync(Model.Issue aggregate)
        {
            dbContext.Attach(aggregate.Reporter);
            if (aggregate.Assignee != null)
                dbContext.Attach(aggregate.Assignee);

            return base.AddAsync(aggregate);
        }

        public override Task<Model.Issue> FindAsync(Guid id)
        {
            return Query.Include(x => x.Reporter).Include(x => x.Assignee).Include(x => x.Labels).Include(x => x.Subtasks).SingleOrDefaultAsync(x => x.Id == id);
        }

        public override Task<Model.Issue> GetAsync(Guid id)
        {
            return Query.Include(x => x.Reporter).Include(x => x.Assignee).Include(x => x.Labels).Include(x => x.Subtasks).SingleOrDefaultAsync(x => x.Id == id) ??
                throw new EntityDoesNotExist(id, nameof(Model.Issue));
        }
    }
}
