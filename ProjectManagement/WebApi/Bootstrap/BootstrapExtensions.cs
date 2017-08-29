using Autofac;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ProjectManagement;

namespace WebApi.Bootstrap
{
    public static class BootstrapExtensions
    {
        public static void RegisterAppModules(this ContainerBuilder builder, IConfigurationRoot configuration, ILoggerFactory loggerFactory)
        {
            new ProjectManagementBootstrap(builder, configuration, loggerFactory);
        }
    }
}
