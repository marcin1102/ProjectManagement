using System;
using System.Collections.Generic;
using Autofac;
using Infrastructure.Bootstrap;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ProjectManagement;
using ProjectManagementView;
using UserManagement;
using Microsoft.Extensions.DependencyInjection;

namespace WebApi.Bootstrap
{
    public static class BootstrapExtensions
    {
        public static void RegisterAppModules(this IServiceCollection services, IConfigurationRoot configuration, ILoggerFactory loggerFactory)
        {
            var projectManagementBootstrap = new ProjectManagementBootstrap(services, configuration, loggerFactory);
            var userManagementBootstrap = new UserManagementBootstrap(services, configuration, loggerFactory);
            //var projectManagementViewBootstrap = new ProjectManagementViewsBootstrap(builder, configuration, loggerFactory);

            services.AddSingleton<ModuleBootstrap>(projectManagementBootstrap);
            services.AddSingleton<ModuleBootstrap>(userManagementBootstrap);
        }

        public static void UseAppModules(this IServiceProvider container)
        {
            var modules = container.GetServices<ModuleBootstrap>();
            //var modules = container.Resolve<IEnumerable<ModuleBootstrap>>();
            foreach (var module in modules)
            {
                module.Run(container);
            }
        }
    }
}
