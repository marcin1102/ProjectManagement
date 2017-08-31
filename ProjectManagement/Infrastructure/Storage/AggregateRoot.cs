using System;
using System.Collections.Generic;
using Infrastructure.Message;

namespace Infrastructure.Storage
{
    public class AggregateRoot : IAggregateRoot
    {
        public Queue<IDomainEvent> PendingEvents { get; private set; }
        public Guid Id { get; private set; }
        public long Version { get; private set; }

        public AggregateRoot()
        {
            PendingEvents = new Queue<IDomainEvent>();
        }

        public void Update(IDomainEvent @event)
        {
            Version = Version++;
            PendingEvents.Enqueue(@event);
        }
    }
}
