﻿using System;
using ProjectManagement.Infrastructure.Primitives.Message;
using ProjectManagement.Contracts.Issue.Enums;
using ProjectManagement.Contracts.Issue.Events;

namespace ProjectManagement.Contracts.Nfr.Events
{
    public class NfrMarkedAsDone : IIssueMarkedAsDone, IDomainEvent
    {
        public NfrMarkedAsDone(Guid id, IssueStatus status)
        {
            Id = id;
            Status = status;
        }

        public Guid Id { get; private set; }
        public IssueStatus Status { get; private set; }
        public long AggregateVersion { get; set; }
    }
}
