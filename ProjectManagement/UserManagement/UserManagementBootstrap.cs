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
using UserManagement.Contracts.User.Commands;
using UserManagement.Contracts.User.Queries;
using UserManagement.User.Handlers;
using UserManagement.User.Repository;

namespace UserManagement
{
    public class UserManagementBootstrap : ModuleBootstrap
    {
        public UserManagementBootstrap(ContainerBuilder builder, IConfigurationRoot configuration, ILoggerFactory logger) : base(builder, configuration, logger)
        {
            RegisterModuleComponents();
            RegisterRepositories();
        }

        private void RegisterRepositories()
        {
            builder
                .RegisterType<UserRepository>()
                .AsSelf()
                .InstancePerLifetimeScope();
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
            RegisterAsyncCommandHandler<CreateUser, UserCommandHandler>();
            RegisterAsyncCommandHandler<GrantRole, UserCommandHandler>();
        }

        public override void RegisterEventHandlers()
        {
        }

        public override void RegisterQueryHandlers()
        {
            RegisterAsyncQueryHandler<GetUser, UserResponse, UserQueryHandler>();
        }
    }
}
