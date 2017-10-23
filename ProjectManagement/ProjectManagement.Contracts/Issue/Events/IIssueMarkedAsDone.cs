using System;
using Infrastructure.Message;
using ProjectManagement.Contracts.Issue.Enums;

namespace ProjectManagement.Contracts.Issue.Events
{
    public interface IIssueMarkedAsDone
    {
        Guid Id { get; }
        IssueStatus Status { get; }
    }
}
