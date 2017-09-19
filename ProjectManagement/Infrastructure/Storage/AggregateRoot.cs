using System;
using System.Collections.Generic;
using Infrastructure.Message;

namespace Infrastructure.Storage
{
    public class AggregateRoot : IAggregateRoot
    {
        private Queue<IDomainEvent> pendingEvents = new Queue<IDomainEvent>();
        public Queue<IDomainEvent> PendingEvents
        {
            get => pendingEvents;
            private set => pendingEvents = new Queue<IDomainEvent>(value);
        }

        public Guid Id { get; private set; }
        public long Version { get; private set; }

        protected AggregateRoot()
        {
        }

        protected AggregateRoot(Guid id)
        {
            Id = id;
            Version = 0;
        }

        public void Update(IDomainEvent @event)
        {
            Version += 1;
            @event.AggregateVersion = Version;
            pendingEvents.Enqueue(@event);
        }

        public virtual void Created() { }
    }
}
