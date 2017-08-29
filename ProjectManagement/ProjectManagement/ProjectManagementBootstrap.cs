using System;
using Infrastructure.Bootstrap;
using Microsoft.Extensions.DependencyInjection;
using Autofac;
using Infrastructure.Message.Handlers;
using ProjectManagement.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Infrastructure.Settings;
using Microsoft.Extensions.Logging;

namespace ProjectManagement
{
    public class ProjectManagementBootstrap : ModuleBootstrap
    {
        private const string SCHEMA = "project-management";

        private readonly IConfigurationRoot configuration;
        private readonly ILoggerFactory logger;

        public ProjectManagementBootstrap(ContainerBuilder builder, IConfigurationRoot configuration, ILoggerFactory logger) : base(builder)
        {
            this.configuration = configuration;
            this.logger = logger;
            RegisterModuleComponents();
        }

        private void RegisterModuleComponents()
        {
            var globalSettings = configuration.GetSection(nameof(GlobalSettings)).Get<GlobalSettings>();

            builder.Register(x =>
            {
                var dbContextOptions = new DbContextOptionsBuilder<ProjectManagementContext>()
               .UseNpgsql(globalSettings.ConnectionString).UseLoggerFactory(logger).Options;
                return new ProjectManagementContext(dbContextOptions, SCHEMA);
            })
            .As<ProjectManagementContext>()
            .InstancePerRequest();
        }

        public override void RegisterCommandHandlers()
        {
            RegisterAsyncCommandHandler<TestCommand, TestCommandHandler>();
        }

        public override void RegisterEventHandlers()
        {

        }

        public override void RegisterQueryHandlers()
        {
            RegisterAsyncQueryHandler<TestQuery, TestResponse, TestQueryHandler>();
        }
    }
}
