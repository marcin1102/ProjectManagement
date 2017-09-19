using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using Infrastructure.Message;

namespace ProjectManagement.Contracts.Label.Queries
{
    public class GetLabels : IQuery<ICollection<LabelResponse>>
    {
        public Guid ProjectId { get; set; }
    }

    public class GetLabelsValidator : AbstractValidator<GetLabels>
    {
        public GetLabelsValidator()
        {
            RuleFor(x => x.ProjectId).NotEmpty();
        }
    }
}
