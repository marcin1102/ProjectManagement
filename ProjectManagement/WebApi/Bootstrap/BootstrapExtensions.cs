using System.Collections.Generic;
using Autofac;
using Infrastructure.Bootstrap;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ProjectManagement;
using ProjectManagementView;
using UserManagement;

namespace WebApi.Bootstrap
{
    public static class BootstrapExtensions
    {
        public static void RegisterAppModules(this ContainerBuilder builder, IConfigurationRoot configuration, ILoggerFactory loggerFactory)
        {
            var projectManagementBootstrap = new ProjectManagementBootstrap(builder, configuration, loggerFactory);
            var userManagementBootstrap = new UserManagementBootstrap(builder, configuration, loggerFactory);
            var projectManagementViewBootstrap = new ProjectManagementViewsBootstrap(builder, configuration, loggerFactory);

            builder
                .RegisterInstance<ProjectManagementBootstrap>(projectManagementBootstrap)
                .As<ModuleBootstrap>()
                .AsSelf();
            builder
                .RegisterInstance<UserManagementBootstrap>(userManagementBootstrap)
                .As<ModuleBootstrap>()
                .AsSelf();

            builder
                .RegisterInstance<ProjectManagementViewsBootstrap>(projectManagementViewBootstrap)
                .As<ModuleBootstrap>()
                .AsSelf();
        }

        public static void UseAppModules(this IContainer container)
        {
            var modules = container.Resolve<IEnumerable<ModuleBootstrap>>();
            foreach (var module in modules)
            {
                module.Run(container);
            }
        }
    }
}
