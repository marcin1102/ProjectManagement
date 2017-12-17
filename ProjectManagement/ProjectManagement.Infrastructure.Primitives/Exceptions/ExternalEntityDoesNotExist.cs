using System;

namespace ProjectManagement.Infrastructure.Primitives.Exceptions
{
    public class ExternalEntityDoesNotExist : EntityDoesNotExist
    {
        public ExternalEntityDoesNotExist(Guid entityId, string entityName) : base(entityId, entityName)
        {
        }

        public override string Message => $"External entity {EntityName} with id `{EntityId}` does not exist";
    }
}
