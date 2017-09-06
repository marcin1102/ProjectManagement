using Infrastructure.Bootstrap;
using Microsoft.Extensions.DependencyInjection;
using Autofac;
using ProjectManagement.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Infrastructure.Settings;
using Microsoft.Extensions.Logging;
using Infrastructure.Storage.EF;
using ProjectManagement.Contracts.Project.Commands;
using ProjectManagement.Project.Handlers;
using System;
using ProjectManagement.Project.Repository;
using UserManagement.Contracts.User.Events;
using ProjectManagement.User.Handlers;
using ProjectManagement.User.Repository;

namespace ProjectManagement
{
    public class ProjectManagementBootstrap : ModuleBootstrap
    {
        private readonly IConfigurationRoot configuration;
        private readonly ILoggerFactory logger;

        public ProjectManagementBootstrap(ContainerBuilder builder, IConfigurationRoot configuration, ILoggerFactory logger) : base(builder)
        {
            this.configuration = configuration;
            this.logger = logger;
            RegisterModuleComponents();
            RegisterRepositories();
        }

        private void RegisterRepositories()
        {
            builder
                .RegisterType<ProjectRepository>()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<UserRepository>()
                .InstancePerLifetimeScope();
        }

        private void RegisterModuleComponents()
        {
            var globalSettings = configuration.GetSection(nameof(GlobalSettings)).Get<GlobalSettings>();

            builder.AddDbContext<ProjectManagementContext>(options =>
            {
                options.UseNpgsql(globalSettings.ConnectionString, x => x.MigrationsAssembly("ProjectManagement")).UseLoggerFactory(logger);
            });
        }

        public override void RegisterCommandHandlers()
        {
            RegisterAsyncCommandHandler<CreateProject, ProjectCommandHandler>();
        }

        public override void RegisterEventHandlers()
        {
            RegisterAsyncEventHandler<UserCreated, UserEventHandler>();
            RegisterAsyncEventHandler<RoleGranted, UserEventHandler>();
        }

        public override void RegisterQueryHandlers()
        {
        }
    }
}
