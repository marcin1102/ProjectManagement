using Infrastructure.Bootstrap;
using Microsoft.Extensions.DependencyInjection;
using Autofac;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Infrastructure.Settings;
using Microsoft.Extensions.Logging;
using Infrastructure.Storage.EF;
using ProjectManagement.Contracts.Project.Commands;
using ProjectManagement.Project.Handlers;
using ProjectManagement.Project.Repository;
using UserManagement.Contracts.User.Events;
using ProjectManagement.User.Handlers;
using ProjectManagement.User.Repository;
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
using ProjectManagement.Services;
using ProjectManagement.Contracts.Sprint.Commands;
using ProjectManagement.Sprint.Handlers;
using ProjectManagement.Sprint.Repository;
using ProjectManagement.Contracts.Sprint.Queries;
using ProjectManagement.Sprint.Searchers;

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
            RegisterServices();
        }

        private void RegisterServices()
        {
            builder
                .RegisterType<AuthorizationService>()
                .As<IAuthorizationService>()
                .InstancePerLifetimeScope();
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

            builder
               .RegisterType<SprintSearcher>()
               .As<ISprintSearcher>()
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

            builder
                .RegisterType<SprintRepository>()
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
            RegisterAsyncCommandHandler<MarkAsInProgress, IssueCommandHandler>();
            RegisterAsyncCommandHandler<MarkAsDone, IssueCommandHandler>();
            RegisterAsyncCommandHandler<AssignAssigneeToIssue, IssueCommandHandler>();
            RegisterAsyncCommandHandler<AssignIssueToSprint, IssueCommandHandler>();

            //Sprint
            RegisterAsyncCommandHandler<CreateSprint, SprintCommandHandler>();
            RegisterAsyncCommandHandler<StartSprint, SprintCommandHandler>();
            RegisterAsyncCommandHandler<FinishSprint, SprintCommandHandler>();
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

            //Sprint
            RegisterAsyncQueryHandler<GetSprint, SprintResponse, SprintQueryHandler>();
        }
    }
}
