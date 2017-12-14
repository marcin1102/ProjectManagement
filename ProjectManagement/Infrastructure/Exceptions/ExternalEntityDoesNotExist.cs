using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Exceptions
{
    public class ExternalEntityDoesNotExist : EntityDoesNotExist
    {
        public ExternalEntityDoesNotExist(Guid entityId, string entityName) : base(entityId, entityName)
        {
        }

        public override string Message => $"External entity {EntityName} with id `{EntityId}` does not exist";
    }
}
