using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Exceptions
{
    public class ConcurrentModificationException : Exception
    {
        public ConcurrentModificationException(Guid entityId, string entityName)
            : base($"{entityName} with id `{entityId}` was already modified. Pull newest version and try again.")
        {
            EntityId = entityId;
            EntityName = entityName;
        }

        public Guid EntityId { get; private set; }
        public string EntityName { get; private set; }
    }
}
