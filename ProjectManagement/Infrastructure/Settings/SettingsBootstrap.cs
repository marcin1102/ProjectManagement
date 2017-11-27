using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Settings
{
    public static class SettingsBootstrap
    {
        public static void RegisterSettings(this IServiceCollection services, IConfigurationRoot configuration)
        {
            services.RegisterGlobalSettings(configuration);
        }

        public static void RegisterGlobalSettings(this IServiceCollection services, IConfigurationRoot configuration)
        {
            services.AddTransient(x =>
            {
                return configuration.GetSection("GlobalSettings").Get<GlobalSettings>();
            });
        }
    }
}
