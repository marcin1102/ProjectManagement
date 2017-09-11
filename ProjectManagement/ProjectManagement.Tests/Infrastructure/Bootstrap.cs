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
using NSubstitute;
using ProjectManagement.Contracts.Label.Commands;
using ProjectManagement.Contracts.Label.Queries;
using ProjectManagement.Contracts.Project.Commands;
using ProjectManagement.Contracts.Project.Queries;
using ProjectManagement.Label.Handlers;
using ProjectManagement.Label.Repository;
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
        private DbContextOptions<ProjectManagementContext> options;
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

            RegisterSubstitutes(builder);

            context = builder.Build();

            RegisterCommandPipelines();
            EnsureDatabaseIsClear();

            return new ProjectManagementModule(
                context: context
            );
        }

        private void RegisterSubstitutes(ContainerBuilder builder)
        {
            var genericFakeCommandPipelineItem = typeof(FakeCommandPIpelineItem<>);

            builder
                .RegisterGeneric(genericFakeCommandPipelineItem)
                .InstancePerLifetimeScope();

            builder
                .RegisterType<FakePipelineItemsConfiguration>()
                .As<IPipelineItemsConfiguration>()
                .SingleInstance();
        }

        private void EnsureDatabaseIsClear()
        {
            using(var db = new ProjectManagementContext(options))
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
            }
        }

        public void RegisterDbContext(ContainerBuilder builder, string connectionString)
        {
            var dbContextOptionsBuilder = new DbContextOptionsBuilder<ProjectManagementContext>();
            dbContextOptionsBuilder.UseNpgsql(connectionString: connectionString);
            options = dbContextOptionsBuilder.Options;

            builder.RegisterInstance<DbContextOptions<ProjectManagementContext>>(options);

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

            builder
                .RegisterType<LabelRepository>()
                .InstancePerLifetimeScope();
        }

        public override void RegisterCommandHandlers()
        {
            //Project
            RegisterAsyncCommandHandler<CreateProject, ProjectCommandHandler>();
            RegisterAsyncCommandHandler<AssignUserToProject, ProjectCommandHandler>();

            //Label
            RegisterAsyncCommandHandler<CreateLabel, LabelCommandHandler>();
        }

        public override void RegisterQueryHandlers()
        {
            //Project
            RegisterAsyncQueryHandler<GetProject, ProjectResponse, ProjectQueryHandler>();

            //Label
            RegisterAsyncQueryHandler<GetLabel, LabelResponse, LabelQueryHandler>();
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
            var defaultCommandPipeline = new List<Type> { typeof(FakeCommandPIpelineItem<>) };
            var pipelineConfiguration = context.Resolve<IPipelineItemsConfiguration>();

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
