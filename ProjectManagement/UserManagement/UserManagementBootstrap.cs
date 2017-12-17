using System.Reflection;
using Autofac;
using ProjectManagement.Infrastructure.Bootstrap;
using ProjectManagement.Infrastructure.Providers;
using ProjectManagement.Infrastructure.Settings;
using ProjectManagement.Infrastructure.Storage.EF;
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
using UserManagement.Authentication;
using System.Linq;
using UserManagement.User.Services;

namespace UserManagement
{
    public class UserManagementBootstrap : ModuleBootstrap
    {
        private readonly string adminEmail;
        private readonly string adminPassword;

        public UserManagementBootstrap(ContainerBuilder builder, IConfigurationRoot configuration, ILoggerFactory logger) : base(builder, configuration, logger)
        {
            RegisterModuleComponents();
            RegisterRepositories();
            RegisterSearchers();
            RegisterServices();

            var uberAdmin = configuration.GetSection(nameof(UberAdmin)).Get<UberAdmin>();
            adminEmail = uberAdmin.Email;
            adminPassword = uberAdmin.Password;
        }

        private void RegisterServices()
        {
            builder
                .RegisterType<TokenFactory>()
                .As<ITokenFactory>()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<AuthTokenStore>()
                .AsSelf()
                .SingleInstance();

            builder
                .RegisterType<UserFactory>()
                .As<IUserFactory>()
                .InstancePerLifetimeScope();
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

        public override void Run(IComponentContext context)
        {
            var tokenStore = context.Resolve<AuthTokenStore>();
            var db = context.Resolve<UserManagementContext>();
            var today = DateTimeOffset.UtcNow;
            var tokens = db.Tokens.Where(x => x.LastlyUsed.Date == today.Date && x.LastlyUsed.TimeOfDay >= today.TimeOfDay.Subtract(new TimeSpan(0, 15, 0))).ToList();
            tokenStore.InitializeWithValuesFromDatabase(tokens.Select(x => TokenFactory.ToAccessToken(x)));

            InitializeSystemWithAdminIfNeeded(context, db);

            db.Dispose();
            base.Run(context);
        }

        private void InitializeSystemWithAdminIfNeeded(IComponentContext context, UserManagementContext db)
        {            
            var isAmin = db.Users.Any(x => x.Role == Contracts.User.Enums.Role.Admin);
            if (!isAmin)
            {
                var repository = context.Resolve<UserRepository>();
                var userFactory = context.Resolve<IUserFactory>();

                repository.AddAsync(userFactory.Create("Admin", "Admin", adminEmail, adminPassword, Contracts.User.Enums.Role.Admin)).Wait() ;
            }
        }
    }
}
