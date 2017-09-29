using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Infrastructure.Bootstrap;
using Infrastructure.Settings;
using Infrastructure.Storage.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

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
            //throw new NotImplementedException();
        }

        public override void RegisterQueryHandlers()
        {
            //throw new NotImplementedException();
        }
    }
}
