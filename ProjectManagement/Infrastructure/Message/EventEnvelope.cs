using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Storage.EF;

namespace Infrastructure.Message
{
    public class EventEnvelope : IEntity
    {
        public Guid Id { get; private set; }
        public IDomainEvent DomainEvent { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public EventEnvelope(IDomainEvent @event)
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.Now;
            DomainEvent = @event;
        }
    }
}
