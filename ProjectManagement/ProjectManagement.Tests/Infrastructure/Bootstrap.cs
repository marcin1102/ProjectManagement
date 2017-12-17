using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using ProjectManagement.Infrastructure.Message;
using ProjectManagement.Infrastructure.Message.Pipeline.PipelineItems;
using ProjectManagement.Infrastructure.Message.Pipeline.PipelineItems.CommandPipelineItems;
using ProjectManagement.Infrastructure.Providers;
using ProjectManagement.Infrastructure.Settings;
using ProjectManagement.Infrastructure.Storage.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ProjectManagement.Contracts.Project.Commands;

namespace ProjectManagement.Tests.Infrastructure
{
    public class Bootstrap : ProjectManagementBootstrap
    {
        private DbContextOptions<ProjectManagementContext> options;
        public Bootstrap(ContainerBuilder builder, IConfigurationRoot configuration, ILoggerFactory logger) : base(builder, configuration, logger)
        {
        }

        internal ProjectManagementModule Build()
        {
            var globalSettings = configuration.GetSection("GlobalSettings").Get<GlobalSettings>();

            RegisterDbContext(builder, globalSettings.ConnectionString);

            builder
                .RegisterType<LoggerFactory>()
                .As<ILoggerFactory>();

            builder.RegisterMessagingComponents();

            RegisterSubstitutes(builder);

            context = builder.Build();

            RegisterCommandPipelines();
            EnsureDatabaseIsClear();

            return new ProjectManagementModule(
                context: context
            );
        }

        private void RegisterSubstitutes(ContainerBuilder builder)
        {
            var genericFakeCommandPipelineItem = typeof(FakeCommandPIpelineItem<>);

            builder
                .RegisterGeneric(genericFakeCommandPipelineItem)
                .InstancePerLifetimeScope();

            builder
                .RegisterType<FakePipelineItemsConfiguration>()
                .As<IPipelineItemsConfiguration>()
                .SingleInstance();
        }

        private void EnsureDatabaseIsClear()
        {
            using(var db = new ProjectManagementContext(options))
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
            }
        }

        public void RegisterDbContext(ContainerBuilder builder, string connectionString)
        {
            var dbContextOptionsBuilder = new DbContextOptionsBuilder<ProjectManagementContext>();
            dbContextOptionsBuilder.UseNpgsql(connectionString: connectionString);
            options = dbContextOptionsBuilder.Options;

            builder.RegisterInstance<DbContextOptions<ProjectManagementContext>>(options);

            builder.Register<ProjectManagementContext>(x =>
            {
                var options = x.Resolve<DbContextOptions<ProjectManagementContext>>();
                return new ProjectManagementContext(options);
            })
            .As<DbContext>()
            .As<BaseDbContext>()
            .AsSelf()
            .InstancePerDependency();
        }

        public override void AddAssemblyToProvider()
        {
            AssembliesProvider.assemblies.Add(typeof(ProjectManagementBootstrap).GetTypeInfo().Assembly);
        }
    }
}
