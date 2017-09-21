using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Exceptions;
using ProjectManagement.Contracts.Issue.Enums;

namespace ProjectManagement.Contracts.DomainExceptions
{
    public class CannotChangeIssueStatus : DomainException
    {
        public CannotChangeIssueStatus(Guid issueId, Status currentStatus, Status expectedStatus, string domainName) :
            base(domainName, $"Cannot change status of issue with id {issueId} to {expectedStatus.ToString()} because of current status. [ {currentStatus.ToString()} ]")
        {
        }
    }
}
