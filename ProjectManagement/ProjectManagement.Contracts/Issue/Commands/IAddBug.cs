using System;
using ProjectManagement.Infrastructure.Primitives.Message;

namespace ProjectManagement.Contracts.Issue.Commands
{
    public interface IAddBug : ICommand
    {
        Guid BugId { get; }
        Guid IssueId { get; }
    }
}
