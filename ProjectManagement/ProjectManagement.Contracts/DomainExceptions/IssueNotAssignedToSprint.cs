using ProjectManagement.Infrastructure.Primitives.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManagement.Contracts.DomainExceptions
{
    public class IssueNotAssignedToSprint : DomainException
    {
        public IssueNotAssignedToSprint(Guid issueId, string domainName) :
            base(domainName, $"Issue with id {issueId} is not assigned to sprint.")
        {
        }
    }
}
