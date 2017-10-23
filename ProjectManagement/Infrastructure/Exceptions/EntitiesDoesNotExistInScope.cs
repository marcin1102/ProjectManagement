using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Exceptions
{
    public class EntitiesDoesNotExistInScope : EntityDoesNotExist
    {
        public string ScopeEntityName { get; private set; }
        public Guid ScopeEntityId { get; private set; }
        public ICollection<Guid> EntitiesIds { get; private set; }

        public EntitiesDoesNotExistInScope(ICollection<Guid> entitiesIds, string entityName, string scopeEntityName, Guid scopeEntityId)
            : base($"")
        {
            EntitiesIds = entitiesIds;
            ScopeEntityId = scopeEntityId;
            ScopeEntityName = scopeEntityName;
        }

        public override string Message => $"Few of {EntityName} do not exist in scope of a {ScopeEntityName} with id {ScopeEntityId}. Not found ids: \n{String.Join('\n', EntitiesIds)}";
    }
}
