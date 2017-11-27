using Autofac;
using Infrastructure.WebApi.Exceptions;
using Infrastructure.WebApi.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.WebApi
{
    public static class MvcBootstrap
    {
        public static void AddMvcFilters(this IServiceCollection services)
        {
            services.AddScoped<RequestValidationFilter>();
            services.AddScoped<ExceptionFilter>();
        }

        public static void AddFilters(this MvcOptions options)
        {
            options.Filters.Add(typeof(RequestValidationFilter), FilterScope.Global);
            options.Filters.Add(typeof(ExceptionFilter), FilterScope.Last);
        }
    }
}
