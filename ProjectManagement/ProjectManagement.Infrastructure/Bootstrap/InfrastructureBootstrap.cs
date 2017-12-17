using Autofac;
using ProjectManagement.Infrastructure.CallContexts;
using ProjectManagement.Infrastructure.Message;
using ProjectManagement.Infrastructure.Settings;
using ProjectManagement.Infrastructure.Storage.EF;
using ProjectManagement.Infrastructure.WebApi;
using Microsoft.Extensions.Configuration;

namespace ProjectManagement.Infrastructure.Bootstrap
{
    public static class InfrastructureBootstrap
    {
        public static void RegisterInfrastructureComponents(this ContainerBuilder builder, IConfigurationRoot configuration)
        {
            builder.RegisterSettings(configuration);
            builder.RegisterEfComponents(configuration);
            builder.RegisterMessagingComponents();
            builder.AddMvcFilters();
            builder.RegisterCallContext();
        }
    }
}
