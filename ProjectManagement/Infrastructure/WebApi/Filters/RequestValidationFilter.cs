using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation.Results;
using Infrastructure.WebApi.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Infrastructure.WebApi.Filters
{
    public class RequestValidationFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ModelState.IsValid)
                return;

            var validationFailures = new List<ValidationFailure>();

            foreach (var modelState in context.ModelState)
            {
                foreach (var error in modelState.Value.Errors)
                {
                    if (error.Exception is ValidationFailureException vf)
                        validationFailures.Add(vf.ValidationFailure);
                    else
                    {
                        var errorMessage = string.IsNullOrEmpty(error.ErrorMessage) ? error.Exception.Message : error.ErrorMessage;
                        var failure = new ValidationFailure(modelState.Key, errorMessage, modelState.Value.AttemptedValue);
                        validationFailures.Add(failure);
                    }
                }
            }
            context.ModelState.AddModelError("FE", "model error");
            context.Result = new ObjectResult(validationFailures)
            {
                StatusCode = 400
            };
        }
    }
}
