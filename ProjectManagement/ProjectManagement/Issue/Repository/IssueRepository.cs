using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Exceptions;
using Infrastructure.Message.EventDispatcher;
using Infrastructure.Storage.EF.Repository;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Issue.Model;

namespace ProjectManagement.Issue.Repository
{
    public class IssueRepository : AggregateRepository<Model.Issue>
    {
        public IssueRepository(ProjectManagementContext dbContext, IEventManager eventManager) : base(dbContext, eventManager)
        {
        }

        public override Task AddAsync(Model.Issue aggregate, long originalVersion)
        {
            dbContext.Attach(aggregate.Reporter);
            if (aggregate.Assignee != null)
                dbContext.Attach(aggregate.Assignee);

            return base.AddAsync(aggregate, originalVersion);
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
