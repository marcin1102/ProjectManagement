using Autofac;
using Infrastructure.Message;
using Infrastructure.Settings;
using Infrastructure.Storage.EF;
using Infrastructure.WebApi;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Bootstrap
{
    public static class InfrastructureBootstrap
    {
        public static void RegisterInfrastructureComponents(this IServiceCollection services, IConfigurationRoot configuration)
        {
            services.RegisterSettings(configuration);
            services.RegisterMessagingComponents();
            services.AddMvcFilters();
        }
    }
}
