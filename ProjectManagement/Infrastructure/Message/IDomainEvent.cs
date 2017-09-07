using System;

namespace Infrastructure.Message
{
    public interface IDomainEvent
    {
        long AggregateVersion { get; set; }
    }
}
