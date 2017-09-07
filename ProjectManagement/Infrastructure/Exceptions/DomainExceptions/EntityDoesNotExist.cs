using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Exceptions.DomainExceptions
{
    public class EntityDoesNotExist : Exception
    {
        public Guid EntityId { get; private set; }
        public string EntityName { get; private set; }

        public EntityDoesNotExist(Guid entityId, string entityName)
            : base($"{entityName} with id `{entityId}` does not exist")
        {
            EntityId = entityId;
            EntityName = entityName;
        }
    }
}
