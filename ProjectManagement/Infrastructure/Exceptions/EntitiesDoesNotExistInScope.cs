using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Exceptions
{
    public class EntitiesDoesNotExistInScope : EntityDoesNotExist
    {
        public string scopeEntityName { get; private set; }
        public Guid scopeEntityId { get; private set; }
        public ICollection<Guid> entitiesIds { get; private set; }

        public EntitiesDoesNotExistInScope(ICollection<Guid> entitiesIds, string entityName, string scopeEntityName, Guid scopeEntityId)
            : base($"")
        {
            this.entitiesIds = entitiesIds;
            this.scopeEntityId = scopeEntityId;
            this.scopeEntityName = scopeEntityName;
        }

        public override string Message => $"{EntityName}s do not exist in scope of {scopeEntityName} with id {scopeEntityId}. Ids: \n{String.Join('\n', entitiesIds)}";
    }
}
