using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using Infrastructure.Bootstrap;
using Infrastructure.Message;
using Infrastructure.Message.CommandQueryBus;
using Infrastructure.Message.EventDispatcher;
using Infrastructure.Message.Pipeline.PipelineItems;
using Infrastructure.Message.Pipeline.PipelineItems.CommandPipelineItems;
using Infrastructure.Providers;
using Infrastructure.Settings;
using Infrastructure.Storage.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ProjectManagement.Contracts.Project.Commands;
using ProjectManagement.Contracts.Project.Queries;
using ProjectManagement.PipelineItems;
using ProjectManagement.Project.Handlers;
using ProjectManagement.Project.Repository;
using ProjectManagement.User.Handlers;
using ProjectManagement.User.Repository;
using UserManagement.Contracts.User.Events;

namespace ProjectManagement.Tests.Infrastructure
{
    public class Bootstrap : ModuleBootstrap
    {
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
            RegisterRepositories(builder);

            context = builder.Build();

            RegisterCommandPipelines();
            EnsureDatabaseIsClear(context);

            return new ProjectManagementModule(
                context: context
            );
        }

        private void EnsureDatabaseIsClear(IComponentContext context)
        {
            var db = context.Resolve<DbContext>();
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            db.Dispose();
        }

        public void RegisterDbContext(ContainerBuilder builder, string connectionString)
        {
            builder.Register<DbContextOptions<ProjectManagementContext>>(x =>
            {
                var dbContextOptionsBuilder = new DbContextOptionsBuilder<ProjectManagementContext>();
                dbContextOptionsBuilder.UseNpgsql(connectionString: connectionString);
                return dbContextOptionsBuilder.Options;
            })
           .InstancePerDependency();

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

        public void RegisterRepositories(ContainerBuilder builder)
        {
            builder
               .RegisterType<ProjectRepository>()
               .InstancePerDependency();

            builder
                .RegisterType<UserRepository>()
                .InstancePerDependency();
        }

        public override void RegisterCommandHandlers()
        {
            RegisterAsyncCommandHandler<CreateProject, ProjectCommandHandler>();
            RegisterAsyncCommandHandler<AssignUserToProject, ProjectCommandHandler>();
        }

        public override void RegisterQueryHandlers()
        {
            RegisterAsyncQueryHandler<GetProject, ProjectResponse, ProjectQueryHandler>();
        }

        public override void RegisterEventHandlers()
        {
            RegisterAsyncEventHandler<UserCreated, UserEventHandler>();
            RegisterAsyncEventHandler<RoleGranted, UserEventHandler>();
        }

        public override void RegisterPipelineItems()
        {
            builder
                .RegisterGeneric(typeof(UserAuthorizationPipelineItem<>))
                .InstancePerDependency();
        }

        public override void RegisterCommandPipelines()
        {
            var defaultCommandPipeline = PredefinedCommandPipelines.TransactionalCommandExecutionPipeline.ToList();
            var pipelineConfiguration = context.Resolve<PipelineItemsConfiguration>();

            var authorizationPipeline = new List<Type>
            {
                typeof(UserAuthorizationPipelineItem<>)
            }.Concat(defaultCommandPipeline);

            pipelineConfiguration.SetCommandPipeline<CreateProject>(authorizationPipeline);
            pipelineConfiguration.SetCommandPipeline<AssignUserToProject>(authorizationPipeline);
        }

        public override void AddAssemblyToProvider()
        {
            AssembliesProvider.assemblies.Add(typeof(ProjectManagementBootstrap).GetTypeInfo().Assembly);
        }
    }
}
