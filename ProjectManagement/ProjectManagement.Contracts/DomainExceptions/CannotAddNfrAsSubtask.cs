using System;
using System.Collections.Generic;
using System.Text;
using ProjectManagement.Infrastructure.Primitives.Exceptions;

namespace ProjectManagement.Contracts.DomainExceptions
{
    public class CannotAddNfrAsSubtask : DomainException
    {
        public CannotAddNfrAsSubtask(string domainName) :
            base(domainName, $"Nfr cannot be added as a subtask to another task.")
        {
        }
    }
}
