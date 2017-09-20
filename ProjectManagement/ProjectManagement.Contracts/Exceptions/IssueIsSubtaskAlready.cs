using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Exceptions;

namespace ProjectManagement.Contracts.Exceptions
{
    public class IssueIsSubtaskAlready : DomainException
    {
        public IssueIsSubtaskAlready(Guid mainIssueId, Guid subtaskId, string domainName) :
            base(domainName, $"An issue with id {subtaskId} is already subtask to an issue with id {mainIssueId}")
        {
        }
    }
}
