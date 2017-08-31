using System;

namespace Infrastructure.Message
{
    public interface IDomainEvent
    {
        Guid Id { get; }
        long AggregateVersion { get; }
    }
}
