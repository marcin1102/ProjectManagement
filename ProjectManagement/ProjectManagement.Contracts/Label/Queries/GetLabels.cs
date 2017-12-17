using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using ProjectManagement.Infrastructure.Primitives.Message;

namespace ProjectManagement.Contracts.Label.Queries
{
    public class GetLabels : IQuery<ICollection<LabelResponse>>
    {
        public GetLabels(Guid projectId)
        {
            ProjectId = projectId;
        }

        public Guid ProjectId { get; private set; }
    }

    public class GetLabelsValidator : AbstractValidator<GetLabels>
    {
        public GetLabelsValidator()
        {
            RuleFor(x => x.ProjectId).NotEmpty();
        }
    }
}
