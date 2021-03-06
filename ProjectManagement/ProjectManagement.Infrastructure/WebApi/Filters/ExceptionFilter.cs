﻿using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using ProjectManagement.Infrastructure.Primitives.Exceptions;

namespace ProjectManagement.Infrastructure.WebApi.Exceptions
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        private readonly ILogger logger;

        public ExceptionFilter(ILogger<ExceptionFilter> logger)
        {
            this.logger = logger;
        }

        public override void OnException(ExceptionContext context)
        {
            switch (context.Exception)
            {
                case EntityDoesNotExist ex:
                    logger.LogDebug(ex.Message, ex);
                    context.Result = new ObjectResult(ex.Message)
                    {
                        StatusCode = (int)HttpStatusCode.BadRequest
                    };
                    break;
                case NotAuthorized ex:
                    logger.LogDebug(ex.Message, ex);
                    context.Result = new ObjectResult(ex.Message)
                    {
                        StatusCode = (int)HttpStatusCode.Forbidden
                    };
                    break;
                case ConcurrentModificationException ex:
                    logger.LogDebug(ex.Message, ex);
                    context.Result = new ObjectResult(ex.Message)
                    {
                        StatusCode = (int)HttpStatusCode.Conflict
                    };
                    break;
                case DomainException ex:
                    logger.LogDebug(ex.Message, ex);
                    context.Result = new ObjectResult(ex.GetResult())
                    {
                        StatusCode = (int)HttpStatusCode.BadRequest
                    };
                    break;
                default:
                    logger.LogDebug(context.Exception.Message, context.Exception);
                    context.Result = new ObjectResult(context.Exception.InnerException)
                    {
                        StatusCode = (int)HttpStatusCode.InternalServerError
                    };
                    break;
            }
        }
    }
}
