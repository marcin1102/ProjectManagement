﻿using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Exceptions;

namespace ProjectManagement.Contracts.Exceptions
{
    public class CannotAddNfrAsSubtask : DomainException
    {
        public CannotAddNfrAsSubtask(string domainName) :
            base(domainName, $"Nfr cannot be added as a subtask to another task.")
        {
        }
    }
}
