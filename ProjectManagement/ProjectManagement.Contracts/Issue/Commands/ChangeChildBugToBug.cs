using ProjectManagement.Infrastructure.Primitives.Message;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManagement.Contracts.Issue.Commands
{
    public interface IChangeChildBugToBug : ICommand
    {
        Guid ProjectId { get; }
        Guid ChildBugId { get; }
    }
}
