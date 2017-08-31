﻿using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Message;

namespace ProjectManagement.Contracts
{
    public class TestDomainEvent : IDomainEvent
    {
        public TestDomainEvent(Guid aggregateId, long aggregateVersion)
        {
            Id = aggregateId;
            AggregateVersion = aggregateVersion;
        }

        public Guid Id { get; private set; }
        public long AggregateVersion { get; private set; }
    }
}
