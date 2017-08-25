using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Storage.EF
{
    public interface IEntity
    {
        Guid Id { get; }
    }
}
