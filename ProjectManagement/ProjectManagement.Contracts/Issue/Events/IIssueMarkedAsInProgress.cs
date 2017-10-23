using System;
using Infrastructure.Message;
using ProjectManagement.Contracts.Issue.Enums;

namespace ProjectManagement.Contracts.Issue.Events
{
    public interface IIssueMarkedAsInProgress
    {
        Guid Id { get; }
        IssueStatus Status { get; }
    }
}
