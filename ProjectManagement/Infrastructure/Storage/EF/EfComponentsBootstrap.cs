using Autofac;
using Infrastructure.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Storage.EF
{
    public static class EfComponentsBootstrap
    {
        public static void RegisterEfComponents(this ContainerBuilder builder, IConfigurationRoot configuration)
        {
            builder.RegisterDbContext(configuration);
        }

        public static void RegisterDbContext(this ContainerBuilder builder, IConfigurationRoot configuration)
        {
            var globalSettings = configuration.GetSection(nameof(GlobalSettings)).Get<GlobalSettings>();
            builder.Register(x =>
            {
                var dbContextOptions = new DbContextOptionsBuilder()
                .UseNpgsql(globalSettings.ConnectionString).Options;
                return new DbContext(dbContextOptions);
            }).As<DbContext>().InstancePerRequest();
        }
    }
}
