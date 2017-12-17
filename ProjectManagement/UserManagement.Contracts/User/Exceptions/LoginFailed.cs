using ProjectManagement.Infrastructure.Primitives.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace UserManagement.Contracts.User.Exceptions
{
    public class LoginFailed : DomainException
    {
        public LoginFailed(string domainName, string message) : base(domainName, message)
        {
        }
    }
}
