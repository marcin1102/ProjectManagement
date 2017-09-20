using System;

namespace Infrastructure.Exceptions
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

        public EntityDoesNotExist(string message)
            : base(message)
        {
        }
    }
}
