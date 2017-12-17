using Autofac;
using ProjectManagement.Infrastructure.WebApi.Exceptions;
using ProjectManagement.Infrastructure.WebApi.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ProjectManagement.Infrastructure.WebApi
{
    public static class MvcBootstrap
    {
        public static void AddMvcFilters(this ContainerBuilder builder)
        {
            builder
                .RegisterType<RequestValidationFilter>()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<ExceptionFilter>()
                .InstancePerLifetimeScope();
        }

        public static void AddFilters(this MvcOptions options)
        {
            options.Filters.Add(typeof(RequestValidationFilter), FilterScope.Global);
            options.Filters.Add(typeof(ExceptionFilter), FilterScope.Last);
        }
    }
}
