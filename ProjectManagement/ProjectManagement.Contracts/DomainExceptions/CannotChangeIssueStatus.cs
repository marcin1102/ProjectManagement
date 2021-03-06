﻿using System;
using ProjectManagement.Infrastructure.Primitives.Exceptions;
using ProjectManagement.Contracts.Issue.Enums;

namespace ProjectManagement.Contracts.DomainExceptions
{
    public class CannotChangeIssueStatus : DomainException
    {
        public CannotChangeIssueStatus(Guid issueId, IssueStatus currentStatus, IssueStatus expectedStatus, string domainName) :
            base(domainName, $"Cannot change status of issue with id {issueId} to {expectedStatus.ToString()} because of current status. [ {currentStatus.ToString()} ]")
        {
        }
    }
}
