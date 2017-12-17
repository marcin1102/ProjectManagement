using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation.Results;

namespace ProjectManagement.Infrastructure.WebApi.Filters
{
    public class ValidationFailureException : Exception
    {
        public ValidationFailureException(ValidationFailure validationFailure)
        {
            ValidationFailure = validationFailure;
        }

        public ValidationFailure ValidationFailure { get; private set; }
    }
}
