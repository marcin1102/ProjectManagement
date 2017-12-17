using System;
using System.Collections.Generic;
using System.Text;
using ProjectManagement.Infrastructure.Primitives.Exceptions;

namespace ProjectManagement.Contracts.DomainExceptions
{
    public class NfrsCanHaveOnlyBugs : DomainException
    {
        public NfrsCanHaveOnlyBugs(string domainName) :
            base(domainName, "Only bugs can be assigned to Nfrs.")
        {
        }
    }
}
