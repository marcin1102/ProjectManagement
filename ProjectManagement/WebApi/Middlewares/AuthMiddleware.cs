using Infrastructure.CallContexts;
using Infrastructure.Exceptions;
using Infrastructure.WebApi.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.Authentication;

namespace WebApi.Middlewares
{
    public class AuthMiddleware
    {
        private readonly RequestDelegate next;

        public AuthMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context, CallContext callContext, AuthTokenStore tokenStore, ILogger<ExceptionFilter> logger)
        {
            if (!context.Request.Path.Value.Contains("api/user-management/users/login") && !context.Request.Path.Value.Contains("swagger/ui") && !context.Request.Path.Value.Contains("swagger/api"))
            {
                var token = context.Request.Headers.SingleOrDefault(x => x.Key == "AccessToken");

                if (string.IsNullOrWhiteSpace(token.Value))
                {
                    await NotAuthorized(context, "Missing authorization token in request");
                    return;
                }

                var isTokenActive = tokenStore.IsTokenActive(token.Value);
                if (!isTokenActive)
                {
                    var accessToken = tokenStore.GetToken(token.Value);
                    tokenStore.RemoveToken(accessToken);
                    await NotAuthorized(context, "Inactive authorization token");
                    return;
                }

                callContext.SetUserId(tokenStore.GetUserByToken(token.Value));
            }

            await next.Invoke(context);
        }

        public async Task NotAuthorized(HttpContext context, string reason)
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync($"Not authorized! {reason}");
        }
    }

}
