using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Storage.EF;
using Newtonsoft.Json;

namespace Infrastructure.Message
{
    public class EventEnvelope : IEntity
    {
        public Guid Id { get; private set; }
        public string DomainEvent { get; private set; }
        public string DomainEventType { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public bool Delivered { get; private set; }

        public EventEnvelope(IDomainEvent @event)
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
            DomainEvent = JsonConvert.SerializeObject(@event);
            DomainEventType = @event.GetType().FullName;
            Delivered = false;
        }

        public void MarkAsDelivered()
        {
            Delivered = true;
        }
    }
}
