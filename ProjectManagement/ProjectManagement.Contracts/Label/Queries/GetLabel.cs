﻿using System;
using FluentValidation;
using ProjectManagement.Infrastructure.Primitives.Message;

namespace ProjectManagement.Contracts.Label.Queries
{
    public class GetLabel : IQuery<LabelResponse>
    {
        public GetLabel(Guid id, Guid projectId)
        {
            Id = id;
            ProjectId = projectId;
        }

        public Guid Id { get; private set; }
        public Guid ProjectId { get; private set; }
    }

    public class LabelResponse
    {
        public LabelResponse(Guid id, Guid projectId, string name, string description)
        {
            Id = id;
            ProjectId = projectId;
            Name = name;
            Description = description;
        }

        public Guid Id { get; private set; }
        public Guid ProjectId { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
    }

    public class GetLabelValidator : AbstractValidator<GetLabel>
    {
        public GetLabelValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
