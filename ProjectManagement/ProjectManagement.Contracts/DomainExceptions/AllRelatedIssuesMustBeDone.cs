using System;
using ProjectManagement.Infrastructure.Primitives.Exceptions;

namespace ProjectManagement.Contracts.DomainExceptions
{
    public class AllRelatedIssuesMustBeDone : DomainException
    {
        public AllRelatedIssuesMustBeDone(Guid issueId, string domainName) :
            base(domainName, $"All issues related to an issue with id {issueId} must be marked as done.")
        {
        }
    }
}
