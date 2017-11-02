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
using ProjectManagement.Contracts.Label.Queries;
using ProjectManagement.Label.Repository;
using ProjectManagement.Label.Searcher;
using ProjectManagement.Project.Searchers;
using ProjectManagement.Issue.Repository;
using ProjectManagement.Issue.Factory;
using ProjectManagement.Services;
using ProjectManagement.Contracts.Sprint.Commands;
using ProjectManagement.Sprint.Handlers;
using ProjectManagement.Sprint.Repository;
using ProjectManagement.Contracts.Sprint.Queries;
using ProjectManagement.Sprint.Searchers;
using ProjectManagement.Issue.Handlers.CommandHandlers;
using ProjectManagement.Contracts.Task.Commands;
using ProjectManagement.Contracts.Nfr.Commands;
using ProjectManagement.task.Handlers.CommandHandlers;
using ProjectManagement.Project.Factory;
using ProjectManagement.Sprint.Factory;
using ProjectManagement.Contracts.Bug.Commands;

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

            builder
               .RegisterType<ProjectFactory>()
               .As<IProjectFactory>()
               .InstancePerLifetimeScope();

            builder
               .RegisterType<SprintFactory>()
               .As<ISprintFactory>()
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
                .RegisterType<TaskRepository>()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<ChildBugRepository>()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<BugRepository>()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<NfrRepository>()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<SubtaskRepository>()
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
            RegisterAsyncCommandHandler<AddLabel, ProjectCommandHandler>();

            //Task
            RegisterAsyncCommandHandler<CreateTask, TaskCommandHandler>();
            RegisterAsyncCommandHandler<AssignLabelsToTask, TaskCommandHandler>();
            RegisterAsyncCommandHandler<CommentTask, TaskCommandHandler>();
            RegisterAsyncCommandHandler<MarkTaskAsInProgress, TaskCommandHandler>();
            RegisterAsyncCommandHandler<MarkTaskAsDone, TaskCommandHandler>();
            RegisterAsyncCommandHandler<AssignAssigneeToTask, TaskCommandHandler>();
            RegisterAsyncCommandHandler<AssignTaskToSprint, TaskCommandHandler>();
            RegisterAsyncCommandHandler<AssignLabelsToTasksBug, TaskCommandHandler>();
            RegisterAsyncCommandHandler<CommentTasksBug, TaskCommandHandler>();
            RegisterAsyncCommandHandler<MarkTasksBugAsInProgress, TaskCommandHandler>();
            RegisterAsyncCommandHandler<MarkTasksBugAsDone, TaskCommandHandler>();
            RegisterAsyncCommandHandler<AssignAssigneeToTasksBug, TaskCommandHandler>();
            RegisterAsyncCommandHandler<AssignTasksBugToSprint, TaskCommandHandler>();
            RegisterAsyncCommandHandler<AddBugToTask, TaskCommandHandler>();

            //Subtask
            RegisterAsyncCommandHandler<AssignLabelsToSubtask, TaskCommandHandler>();
            RegisterAsyncCommandHandler<CommentSubtask, TaskCommandHandler>();
            RegisterAsyncCommandHandler<MarkSubtaskAsInProgress, TaskCommandHandler>();
            RegisterAsyncCommandHandler<MarkSubtaskAsDone, TaskCommandHandler>();
            RegisterAsyncCommandHandler<AssignAssigneeToSubtask, TaskCommandHandler>();
            RegisterAsyncCommandHandler<AssignSubtaskToSprint, TaskCommandHandler>();
            RegisterAsyncCommandHandler<AddSubtaskToTask, TaskCommandHandler>();

            //Nfr
            RegisterAsyncCommandHandler<CreateNfr, NfrCommandHandler>();
            RegisterAsyncCommandHandler<AssignLabelsToNfr, NfrCommandHandler>();
            RegisterAsyncCommandHandler<CommentNfr, NfrCommandHandler>();
            RegisterAsyncCommandHandler<MarkNfrAsInProgress, NfrCommandHandler>();
            RegisterAsyncCommandHandler<MarkNfrAsDone, NfrCommandHandler>();
            RegisterAsyncCommandHandler<AssignAssigneeToNfr, NfrCommandHandler>();
            RegisterAsyncCommandHandler<AssignNfrToSprint, NfrCommandHandler>();
            RegisterAsyncCommandHandler<AssignLabelsToNfrsBug, NfrCommandHandler>();
            RegisterAsyncCommandHandler<CommentNfrsBug, NfrCommandHandler>();
            RegisterAsyncCommandHandler<MarkNfrsBugAsInProgress, NfrCommandHandler>();
            RegisterAsyncCommandHandler<MarkNfrsBugAsDone, NfrCommandHandler>();
            RegisterAsyncCommandHandler<AssignAssigneeToNfrsBug, NfrCommandHandler>();
            RegisterAsyncCommandHandler<AssignNfrsBugToSprint, NfrCommandHandler>();
            RegisterAsyncCommandHandler<AddBugToNfr, NfrCommandHandler>();

            //Sprint
            RegisterAsyncCommandHandler<CreateSprint, SprintCommandHandler>();
            RegisterAsyncCommandHandler<StartSprint, SprintCommandHandler>();
            RegisterAsyncCommandHandler<FinishSprint, SprintCommandHandler>();

            //Bug
            RegisterAsyncCommandHandler<CreateBug, BugCommandHandler>();
            RegisterAsyncCommandHandler<AssignLabelsToBug, BugCommandHandler>();
            RegisterAsyncCommandHandler<CommentBug, BugCommandHandler>();
            RegisterAsyncCommandHandler<MarkBugAsInProgress, BugCommandHandler>();
            RegisterAsyncCommandHandler<MarkBugAsDone, BugCommandHandler>();
            RegisterAsyncCommandHandler<AssignAssigneeToBug, BugCommandHandler>();
            RegisterAsyncCommandHandler<AssignBugToSprint, BugCommandHandler>();
        }

        public override void RegisterEventHandlers()
        {
            RegisterAsyncEventHandler<UserCreated, UserEventHandler>();
            RegisterAsyncEventHandler<RoleGranted, UserEventHandler>();
        }

        public override void RegisterQueryHandlers()
        {
        }
    }
}
