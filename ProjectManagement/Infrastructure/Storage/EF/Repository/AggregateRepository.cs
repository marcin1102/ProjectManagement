using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Message.EventDispatcher;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Storage.EF.Repository
{
    public class AggregateRepository<TAggregate> : Repository<TAggregate>
        where TAggregate : class, IAggregateRoot
    {
        private readonly IEventManager eventManager;

        public AggregateRepository(DbContext dbContext, IEventManager eventManager) : base(dbContext)
        {
            this.eventManager = eventManager;
        }

        public override Task Add(TAggregate aggregate)
        {
            base.Add(aggregate);
            return eventManager.PublishEventsAsync(aggregate.PendingEvents);
        }
    }
}
