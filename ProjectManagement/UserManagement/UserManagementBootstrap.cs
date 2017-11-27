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
using Microsoft.Extensions.DependencyInjection;

namespace UserManagement
{
    public class UserManagementBootstrap : ModuleBootstrap
    {
        public UserManagementBootstrap(IServiceCollection services, IConfigurationRoot configuration, ILoggerFactory logger) : base(services, configuration, logger)
        {
            RegisterModuleComponents();
            RegisterRepositories();
        }

        private void RegisterRepositories()
        {
            services.AddScoped<UserRepository>();
        }

        private void RegisterModuleComponents()
        {
            var globalSettings = configuration.GetSection(nameof(GlobalSettings)).Get<GlobalSettings>();

            services.AddDbContext<UserManagementContext>(options =>
            {
                options.UseNpgsql(globalSettings.ConnectionString).UseLoggerFactory(logger);
            });

            services.AddScoped<BaseDbContext>((x) =>
            {
                return x.GetRequiredService<UserManagementContext>();
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

        public override void AddAssemblyToProvider()
        {
            AssembliesProvider.assemblies.Add(typeof(UserManagementBootstrap).GetTypeInfo().Assembly);
        }
    }
}
