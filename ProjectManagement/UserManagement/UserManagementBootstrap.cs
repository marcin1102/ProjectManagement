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
using ProjectManagement.Contracts;

namespace UserManagement
{
    public class UserManagementBootstrap : ModuleBootstrap
    {
        private readonly IConfigurationRoot configuration;
        private readonly ILoggerFactory logger;

        public UserManagementBootstrap(ContainerBuilder builder, IConfigurationRoot configuration, ILoggerFactory logger) : base(builder)
        {
            this.configuration = configuration;
            this.logger = logger;
            RegisterModuleComponents();
        }

        private void RegisterModuleComponents()
        {
            var globalSettings = configuration.GetSection(nameof(GlobalSettings)).Get<GlobalSettings>();

            builder.AddDbContext<UserManagementContext>(options =>
            {
                options.UseNpgsql(globalSettings.ConnectionString).UseLoggerFactory(logger);
            });
        }

        public override void RegisterCommandHandlers()
        {
        }

        public override void RegisterEventHandlers()
        {
            RegisterAsyncEventHandler<TestDomainEvent, TestEventHandler>();
        }

        public override void RegisterQueryHandlers()
        {
        }
    }
}
