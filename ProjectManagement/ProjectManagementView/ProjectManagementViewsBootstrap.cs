using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Autofac;
using Infrastructure.Bootstrap;
using Infrastructure.Providers;
using Infrastructure.Settings;
using Infrastructure.Storage.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ProjectManagementView.Storage.Handlers;
using UserManagement.Contracts.User.Events;

namespace ProjectManagementView
{
    public class ProjectManagementViewsBootstrap : ModuleBootstrap
    {
        public ProjectManagementViewsBootstrap(ContainerBuilder builder, IConfigurationRoot configuration, ILoggerFactory logger) : base(builder, configuration, logger)
        {
            RegisterModuleComponents();
            RegisterRepositories();
            RegisterSearchers();
            RegisterFactories();
            RegisterServices();
        }

        private void RegisterServices()
        {

        }

        private void RegisterFactories()
        {

        }

        private void RegisterSearchers()
        {

        }

        private void RegisterRepositories()
        {

        }

        private void RegisterModuleComponents()
        {
            var globalSettings = configuration.GetSection(nameof(GlobalSettings)).Get<GlobalSettings>();

            builder.AddDbContext<ProjectManagementViewContext>(options =>
            {
                options.UseNpgsql(globalSettings.ConnectionString, x => x.MigrationsAssembly("ProjectManagementView")).UseLoggerFactory(logger);
            });
        }
        public override void RegisterCommandHandlers()
        {
            //throw new NotImplementedException();
        }

        public override void RegisterEventHandlers()
        {
            //User
            RegisterAsyncEventHandler<UserCreated, UserEventHandler>();
            RegisterAsyncEventHandler<RoleGranted, UserEventHandler>();


        }

        public override void RegisterQueryHandlers()
        {
            //throw new NotImplementedException();
        }

        public override void AddAssemblyToProvider()
        {
            AssembliesProvider.assemblies.Add(typeof(ProjectManagementViewsBootstrap).GetTypeInfo().Assembly);
        }
    }
}
