using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Message.EventDispatcher;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Storage.EF.Repository
{
    public class AggregateRepository<TAggregate>
        where TAggregate : class, IAggregateRoot
    {
        private readonly DbContext dbContext;
        private readonly IEventManager eventManager;

        private readonly DbSet<TAggregate> dbSet;
        public DbSet<TAggregate> Query => dbSet;

        public AggregateRepository(DbContext dbContext, IEventManager eventManager)
        {
            this.dbContext = dbContext;
            this.eventManager = eventManager;
            dbSet = dbContext.Set<TAggregate>();
        }

        public virtual async Task AddAsync(TAggregate aggregate)
        {
            await Query.AddAsync(aggregate);
            await eventManager.PublishEventsAsync(aggregate.PendingEvents);
        }

        public async Task Update(TAggregate entity, long version)
        {
            var aggregate = await GetAsync(entity.Id);
            if (version < aggregate.Version)
                throw new Exception("The aggregate was modified by someone else. Get newest changes and try again");

            Query.Update(entity);
            await dbContext.SaveChangesAsync();
            await eventManager.PublishEventsAsync(aggregate.PendingEvents);
        }

        public virtual Task<TAggregate> GetAsync(Guid id)
        {
            return Query.SingleAsync(x => x.Id == id);
        }

        public virtual Task<TAggregate> FindAsync(Guid id)
        {
            return Query.SingleOrDefaultAsync(x => x.Id == id);
        }
    }
}
