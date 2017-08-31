using System;
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

            builder.AddDbContext<DbContext>(options =>
                options.UseNpgsql(globalSettings.ConnectionString)
                );

            builder.AddDbContext<EventContext.EventContext>(options =>
                options.UseNpgsql(globalSettings.ConnectionString)
                );
        }

        public static void AddDbContext<TContext>(this ContainerBuilder builder, Action<DbContextOptionsBuilder<TContext>> optionsAction)
            where TContext : DbContext
        {
            builder.Register<DbContextOptions<TContext>>(x =>
            {
                var dbContextOptionsBuilder = new DbContextOptionsBuilder<TContext>();
                optionsAction(dbContextOptionsBuilder);
                return dbContextOptionsBuilder.Options;
            })
            .InstancePerLifetimeScope();

            builder
                .Register<TContext>(x =>
                {
                    var dbContextOptions = x.Resolve<DbContextOptions<TContext>>();
                    return (TContext)Activator.CreateInstance(typeof(TContext), dbContextOptions);
                })
                .InstancePerLifetimeScope();
        }
    }
}
