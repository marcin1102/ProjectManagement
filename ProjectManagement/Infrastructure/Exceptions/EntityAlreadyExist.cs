using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Exceptions
{
    public class EntityAlreadyExist : Exception
    {
        public Guid EntityId { get; private set; }
        public string EntityName { get; private set; }

        public EntityAlreadyExist(Guid entityId, string entityName)
            : base($"{entityName} with id `{entityId}` already exist")
        {
            EntityId = entityId;
            EntityName = entityName;
        }

        public EntityAlreadyExist(string message)
            : base(message)
        {
        }
    }
}
