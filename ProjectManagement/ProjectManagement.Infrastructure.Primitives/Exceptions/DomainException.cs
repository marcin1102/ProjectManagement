using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManagement.Infrastructure.Primitives.Exceptions
{
    public class DomainException : Exception
    {
        public DomainException(string domainName, string message) : base(message)
        {
            DomainName = domainName;
        }

        public string DomainName { get; private set; }

        public Result GetResult()
        {
            return new Result(DomainName, Message);
        }

        public class Result
        {
            public Result(string domainName, string message)
            {
                DomainName = domainName;
                Message = message;
            }

            public string DomainName { get; private set; }
            public string Message { get; private set; }
        }
    }
}
