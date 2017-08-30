using System;

namespace Infrastructure.Message
{
    public interface IDomainEvent
    {
        Guid AggregateId { get; }
        long AggregateVersion { get; }
    }
}
