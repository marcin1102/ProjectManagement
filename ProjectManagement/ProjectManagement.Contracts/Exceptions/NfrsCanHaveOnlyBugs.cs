using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Exceptions;

namespace ProjectManagement.Contracts.Exceptions
{
    public class NfrsCanHaveOnlyBugs : DomainException
    {
        public NfrsCanHaveOnlyBugs(string domainName) :
            base(domainName, "Only bugs can be assigned to Nfrs.")
        {
        }
    }
}
