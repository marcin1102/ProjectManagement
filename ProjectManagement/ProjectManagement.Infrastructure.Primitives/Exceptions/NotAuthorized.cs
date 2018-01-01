using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.Infrastructure.Primitives.Exceptions
{
    public class NotAuthorized : Exception
    {
        public NotAuthorized(Guid userId, string commandName)
            : base($"User with id `{userId}` is not authorized to execute action `{commandName}`")
        {
            UserId = userId;
            CommandName = commandName;
        }

        public Guid UserId { get; private set; }
        public string CommandName { get; private set; }
    }
}
