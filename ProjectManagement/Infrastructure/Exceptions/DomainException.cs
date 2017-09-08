using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Exceptions
{
    public class DomainException : Exception
    {
        public DomainException(Guid aggregateId, string aggregateName, string domainName, string message) : base(message)
        {
            AggregateId = aggregateId;
            AggregateName = aggregateName;
            DomainName = domainName;
        }

        public Guid AggregateId { get; private set; }
        public string AggregateName { get; private set; }
        public string DomainName { get; private set; }

        internal Result GetResult()
        {
            return new Result(DomainName, AggregateName, AggregateId, Message);
        }

        internal class Result
        {
            public Result(string domainName, string aggregateName, Guid aggregateId, string message)
            {
                DomainName = domainName;
                AggregateName = aggregateName;
                AggregateId = aggregateId;
                Message = message;
            }

            public string DomainName { get; private set; }
            public string AggregateName { get; private set; }
            public Guid AggregateId { get; private set; }
            public string Message { get; private set; }
        }
    }
}
