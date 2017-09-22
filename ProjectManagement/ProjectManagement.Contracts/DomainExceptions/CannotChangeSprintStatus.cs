using System;
using Infrastructure.Exceptions;
using ProjectManagement.Contracts.Sprint.Enums;

namespace ProjectManagement.Contracts.DomainExceptions
{
    public class CannotChangeSprintStatus : DomainException
    {
        public CannotChangeSprintStatus(Guid sprintId, SprintStatus currentStatus, SprintStatus expectedStatus, string domainName) :
            base(domainName, $"Cannot change status of sprint with id {sprintId} to {expectedStatus.ToString()} because of current status. [ {currentStatus.ToString()} ]")
        {
        }
    }
}
