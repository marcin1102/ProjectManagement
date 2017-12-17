using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManagement.Infrastructure.Storage.EF
{
    public interface IEntity
    {
        Guid Id { get; }
    }
}
