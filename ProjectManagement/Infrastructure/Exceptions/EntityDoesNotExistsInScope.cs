using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Exceptions
{
    public class EntityDoesNotExistsInScope : EntityDoesNotExist
    {
        public EntityDoesNotExistsInScope(Guid entityId, string entityName, string scopeEntity, Guid scopeEntityId)
            : base($"{entityName} with id {entityId} does not exist in scope of {scopeEntity} with id {scopeEntityId}.")
        {
        }
    }
}
