using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Settings
{
    public static class SettingsBootstrap
    {
        public static void RegisterSettings(this ContainerBuilder builder, IConfigurationRoot configuration)
        {
            builder.RegisterGlobalSettings(configuration);
        }

        public static void RegisterGlobalSettings(this ContainerBuilder builder, IConfigurationRoot configuration)
        {
            builder.Register(x =>
            {
                return configuration.GetSection("GlobalSettings").Get<GlobalSettings>();
            })
            .As<GlobalSettings>()
            .InstancePerDependency();
        }
    }
}
