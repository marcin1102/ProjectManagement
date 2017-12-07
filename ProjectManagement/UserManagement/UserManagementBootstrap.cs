using System.Reflection;
using Autofac;
using Infrastructure.Bootstrap;
using Infrastructure.Providers;
using Infrastructure.Settings;
using Infrastructure.Storage.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using UserManagement.Contracts.User.Commands;
using UserManagement.Contracts.User.Queries;
using UserManagement.User.Handlers;
using UserManagement.User.Repository;
using UserManagement.Hashing;
using System;
using UserManagement.User.Searchers;

namespace UserManagement
{
    public class UserManagementBootstrap : ModuleBootstrap
    {
        public UserManagementBootstrap(ContainerBuilder builder, IConfigurationRoot configuration, ILoggerFactory logger) : base(builder, configuration, logger)
        {
            RegisterModuleComponents();
            RegisterRepositories();
            RegisterSearchers();
        }

        private void RegisterSearchers()
        {
            builder
                .RegisterType<UserSearcher>()
                .As<IUserSearcher>()
                .InstancePerLifetimeScope();
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

            builder
                .RegisterType<HashingService>()
                .As<IHashingService>()
                .InstancePerDependency();
        }

        public override void RegisterCommandHandlers()
        {
            RegisterAsyncCommandHandler<CreateUser, UserCommandHandler>();
            RegisterAsyncCommandHandler<GrantRole, UserCommandHandler>();
            RegisterAsyncCommandHandler<Login, UserCommandHandler>();
        }

        public override void RegisterEventHandlers()
        {
        }

        public override void RegisterQueryHandlers()
        {
            RegisterAsyncQueryHandler<GetUser, UserResponse, UserQueryHandler>();
        }

        public override void AddAssemblyToProvider()
        {
            AssembliesProvider.assemblies.Add(typeof(UserManagementBootstrap).GetTypeInfo().Assembly);
        }
    }
}
