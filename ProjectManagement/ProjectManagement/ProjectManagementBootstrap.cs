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
using Infrastructure.Message.Pipeline.PipelineItems.CommandPipelineItems;
using System.Linq;
using Infrastructure.Message.Pipeline.PipelineItems;
using ProjectManagement.PipelineItems;
using System.Collections.Generic;
using ProjectManagement.Contracts.Project.Queries;
using ProjectManagement.Contracts.Label.Commands;
using ProjectManagement.Label.Handlers;
using ProjectManagement.Contracts.Label.Queries;
using ProjectManagement.Label.Repository;
using ProjectManagement.Label.Searcher;
using ProjectManagement.Project.Searchers;
using ProjectManagement.Contracts.Issue.Commands;
using ProjectManagement.Issue.Handlers;
using ProjectManagement.Issue.Repository;
using ProjectManagement.Issue.Factory;
using ProjectManagement.Contracts.Issue.Queries;
using ProjectManagement.Issue.Searchers;

namespace ProjectManagement
{
    public class ProjectManagementBootstrap : ModuleBootstrap
    {
        public ProjectManagementBootstrap(ContainerBuilder builder, IConfigurationRoot configuration, ILoggerFactory logger) : base(builder, configuration, logger)
        {
            RegisterModuleComponents();
            RegisterRepositories();
            RegisterSearchers();
            RegisterFactories();
        }

        private void RegisterFactories()
        {
            builder
               .RegisterType<IssueFactory>()
               .As<IIssueFactory>()
               .InstancePerLifetimeScope();
        }

        private void RegisterSearchers()
        {
            builder
               .RegisterType<ProjectSearcher>()
               .As<IProjectSearcher>()
               .InstancePerLifetimeScope();

            builder
               .RegisterType<LabelSearcher>()
               .As<ILabelsSearcher>()
               .InstancePerLifetimeScope();

            builder
               .RegisterType<IssueSearcher>()
               .As<IIssueSearcher>()
               .InstancePerLifetimeScope();
        }

        private void RegisterRepositories()
        {
            builder
                .RegisterType<ProjectRepository>()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<UserRepository>()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<LabelRepository>()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<IssueRepository>()
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
            //Project
            RegisterAsyncCommandHandler<CreateProject, ProjectCommandHandler>();
            RegisterAsyncCommandHandler<AssignUserToProject, ProjectCommandHandler>();

            //Label
            RegisterAsyncCommandHandler<CreateLabel, LabelCommandHandler>();

            //Issue
            RegisterAsyncCommandHandler<CreateIssue, IssueCommandHandler>();
            RegisterAsyncCommandHandler<AssignLabelsToIssue, IssueCommandHandler>();
            RegisterAsyncCommandHandler<CommentIssue, IssueCommandHandler>();
            RegisterAsyncCommandHandler<AddSubtask, IssueCommandHandler>();
        }

        public override void RegisterEventHandlers()
        {
            RegisterAsyncEventHandler<UserCreated, UserEventHandler>();
            RegisterAsyncEventHandler<RoleGranted, UserEventHandler>();
        }

        public override void RegisterQueryHandlers()
        {
            //Project
            RegisterAsyncQueryHandler<GetProject, ProjectResponse, ProjectQueryHandler>();
            RegisterAsyncQueryHandler<GetProjects, ICollection<ProjectResponse>, ProjectQueryHandler>();

            //Label
            RegisterAsyncQueryHandler<GetLabel, LabelResponse, LabelQueryHandler>();
            RegisterAsyncQueryHandler<GetLabels, ICollection<LabelResponse>, LabelQueryHandler>();

            //Issue
            RegisterAsyncQueryHandler<GetIssue, IssueResponse, IssueQueryHandler>();
            RegisterAsyncQueryHandler<GetIssues, ICollection<IssueListItem>, IssueQueryHandler>();
        }

        public override void RegisterPipelineItems()
        {
            builder
                .RegisterGeneric(typeof(UserAuthorizationPipelineItem<>))
                .InstancePerLifetimeScope();
        }

        public override void RegisterCommandPipelines()
        {
            var defaultCommandPipeline = PredefinedCommandPipelines.TransactionalCommandExecutionPipeline().ToList();
            var pipelineConfiguration = context.Resolve<IPipelineItemsConfiguration>();

            var authorizationPipeline = new List<Type>
            {
                typeof(UserAuthorizationPipelineItem<>)
            }.Concat(defaultCommandPipeline);

            pipelineConfiguration.SetCommandPipeline<CreateProject>(authorizationPipeline);
            pipelineConfiguration.SetCommandPipeline<AssignUserToProject>(authorizationPipeline);
        }
    }
}
