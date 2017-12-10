using Infrastructure.CallContexts;
using Microsoft.AspNetCore.Http;
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

        public async Task Invoke(HttpContext context, CallContext callContext)
        {
            if (!context.Request.Path.Value.Contains("api/user-management/users/login") && !context.Request.Path.Value.Contains("swagger/ui") && !context.Request.Path.Value.Contains("swagger/api"))
            {
                var token = context.Request.Headers.SingleOrDefault(x => x.Key == "AccessToken").Value;
                if (string.IsNullOrWhiteSpace(token))
                    context.Abort();

                var isTokenActive = AuthTokenStore.DoesTokenIsActive(token);
                if (!isTokenActive)
                    context.Abort();

                callContext.SetUserId(AuthTokenStore.GetUserIsByToken(token));
            }

            await next.Invoke(context);
        }
    }

}
