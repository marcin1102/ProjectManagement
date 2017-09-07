using System;
using Infrastructure.Message;

namespace ProjectManagement.Contracts.Interfaces
{
    public interface IAuthorizationRequiredCommand : ICommand
    {
        Guid UserId { get; }
    }
}
