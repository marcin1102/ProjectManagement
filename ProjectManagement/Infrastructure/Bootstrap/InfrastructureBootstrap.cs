using Autofac;
using Infrastructure.CallContexts;
using Infrastructure.Message;
using Infrastructure.Settings;
using Infrastructure.Storage.EF;
using Infrastructure.WebApi;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Bootstrap
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
