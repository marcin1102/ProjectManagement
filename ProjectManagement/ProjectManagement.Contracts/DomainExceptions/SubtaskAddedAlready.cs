using System;
using System.Collections.Generic;
using System.Text;
using ProjectManagement.Infrastructure.Primitives.Exceptions;

namespace ProjectManagement.Contracts.DomainExceptions
{
    public class SubtaskAddedAlready : DomainException
    {
        public SubtaskAddedAlready(Guid mainIssueId, Guid subtaskId, string domainName) :
            base(domainName, $"An issue with id {subtaskId} is already subtask to an issue with id {mainIssueId}")
        {
        }
    }
}
