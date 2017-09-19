﻿using System;
using Infrastructure.Storage.EF;

namespace ProjectManagement.IssueLabel
{
    public class IssueLabel : IEntity
    {
        private IssueLabel()
        { }
        public IssueLabel(Guid issueId, Guid labelId)
        {
            Id = Guid.NewGuid();
            IssueId = issueId;
            LabelId = labelId;
        }

        public Guid Id { get; private set; }
        public Guid IssueId { get; private set; }
        public Guid LabelId { get; private set; }
    }
}
