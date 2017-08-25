using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Storage.EF;

namespace Infrastructure.Storage
{
    public interface IAggregateRoot : IEntity
    {
        long Version { get; }
    }
}
