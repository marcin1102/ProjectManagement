using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using Infrastructure.Message;
using Infrastructure.Message.Pipeline.PipelineItems;
using Infrastructure.Message.Pipeline.PipelineItems.CommandPipelineItems;
using Infrastructure.Providers;
using Infrastructure.Settings;
using Infrastructure.Storage.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ProjectManagement.Contracts.Project.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace ProjectManagement.Tests.Infrastructure
{
    public class Bootstrap : ProjectManagementBootstrap
    {
        private DbContextOptions<ProjectManagementContext> options;
        public Bootstrap(IServiceCollection services, IConfigurationRoot configuration, ILoggerFactory logger) : base(services, configuration, logger)
        {
        }

        internal ProjectManagementModule Build()
        {
            var globalSettings = configuration.GetSection("GlobalSettings").Get<GlobalSettings>();

            RegisterDbContext(services, globalSettings.ConnectionString);

            services.AddTransient<ILoggerFactory, LoggerFactory>();

            services.RegisterMessagingComponents();

            RegisterSubstitutes(services);

            serviceProvider = services.BuildServiceProvider();

            RegisterCommandPipelines();
            EnsureDatabaseIsClear();

            return new ProjectManagementModule(
                serviceProvider: serviceProvider
            );
        }

        private void RegisterSubstitutes(IServiceCollection services)
        {
            var genericFakeCommandPipelineItem = typeof(FakeCommandPIpelineItem<>);

            services.AddScoped(genericFakeCommandPipelineItem);
            services.AddSingleton<IPipelineItemsConfiguration, FakePipelineItemsConfiguration>();
        }

        private void EnsureDatabaseIsClear()
        {
            using(var db = serviceProvider.GetRequiredService<ProjectManagementContext>())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
            }
        }

        public void RegisterDbContext(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ProjectManagementContext>(options =>
            {
                options.UseNpgsql(connectionString: connectionString);
            }, ServiceLifetime.Transient);
        }

        public override void AddAssemblyToProvider()
        {
            AssembliesProvider.assemblies.Add(typeof(ProjectManagementBootstrap).GetTypeInfo().Assembly);
        }
    }
}
