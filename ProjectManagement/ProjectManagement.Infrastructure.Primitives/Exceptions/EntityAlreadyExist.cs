using System;

namespace ProjectManagement.Infrastructure.Primitives.Exceptions
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
