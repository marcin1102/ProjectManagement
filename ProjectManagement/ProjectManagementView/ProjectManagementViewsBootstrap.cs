using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Autofac;
using ProjectManagement.Infrastructure.Bootstrap;
using ProjectManagement.Infrastructure.Providers;
using ProjectManagement.Infrastructure.Settings;
using ProjectManagement.Infrastructure.Storage.EF;
using ProjectManagement.Infrastructure.Storage.EF.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ProjectManagement.Contracts.Bug.Events;
using ProjectManagement.Contracts.Nfr.Events;
using ProjectManagement.Contracts.Project.Events;
using ProjectManagement.Contracts.Subtask.Events;
using ProjectManagement.Contracts.Task.Events;
using ProjectManagementView.Storage.Handlers;
using ProjectManagementView.Storage.Models;
using ProjectManagementView.Storage.Searchers;
using UserManagement.Contracts.User.Events;

namespace ProjectManagementView
{
    public class ProjectManagementViewsBootstrap : ModuleBootstrap
    {
        public ProjectManagementViewsBootstrap(ContainerBuilder builder, IConfigurationRoot configuration, ILoggerFactory logger) : base(builder, configuration, logger)
        {
            RegisterModuleComponents();
            RegisterRepositories();
            RegisterSearchers();
            RegisterFactories();
            RegisterServices();
        }

        private void RegisterServices()
        {

        }

        private void RegisterFactories()
        {

        }

        private void RegisterSearchers()
        {
            builder
                .RegisterType<LabelSearcher>()
                .As<ILabelSearcher>()
                .InstancePerLifetimeScope();
        }

        private void RegisterRepositories()
        {
            builder
                .RegisterType<Repository<Sprint>>()
                .As<IRepository<Sprint>>()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<Repository<Project>>()
                .As<IRepository<Project>>()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<Repository<Task>>()
                .As<IRepository<Task>>()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<Repository<Bug>>()
                .As<IRepository<Bug>>()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<Repository<Subtask>>()
                .As<IRepository<Subtask>>()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<Repository<Nfr>>()
                .As<IRepository<Nfr>>()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<Repository<User>>()
                .As<IRepository<User>>()
                .InstancePerLifetimeScope();
        }

        private void RegisterModuleComponents()
        {
            var globalSettings = configuration.GetSection(nameof(GlobalSettings)).Get<GlobalSettings>();

            builder.AddDbContext<ProjectManagementViewContext>(options =>
            {
                options.UseNpgsql(globalSettings.ConnectionString, x => x.MigrationsAssembly("ProjectManagementView")).UseLoggerFactory(logger);
            });
        }
        public override void RegisterCommandHandlers()
        {
        }

        public override void RegisterEventHandlers()
        {
            //User
            RegisterAsyncEventHandler<UserCreated, UserEventHandler>();
            RegisterAsyncEventHandler<RoleGranted, UserEventHandler>();

            //Project
            RegisterAsyncEventHandler<ProjectCreated, ProjectEventHandler>();
            RegisterAsyncEventHandler<UserAssignedToProject, ProjectEventHandler>();
            RegisterAsyncEventHandler<LabelAdded, ProjectEventHandler>();

            //Task & Subtask
            RegisterAsyncEventHandler<TaskCreated, TaskEventHandler>();
            RegisterAsyncEventHandler<TaskAssignedToSprint, TaskEventHandler>();
            RegisterAsyncEventHandler<AssigneeAssignedToTask, TaskEventHandler>();
            RegisterAsyncEventHandler<LabelAssignedToTask, TaskEventHandler>();
            RegisterAsyncEventHandler<TaskCommented, TaskEventHandler>();
            RegisterAsyncEventHandler<TaskMarkedAsInProgress, TaskEventHandler>();
            RegisterAsyncEventHandler<TaskMarkedAsDone, TaskEventHandler>();
            RegisterAsyncEventHandler<BugAddedToTask, TaskEventHandler>();
            RegisterAsyncEventHandler<SubtaskAddedToTask, TaskEventHandler>();
            RegisterAsyncEventHandler<LabelAssignedToSubtask, TaskEventHandler>();
            RegisterAsyncEventHandler<SubtaskAssignedToSprint, TaskEventHandler>();
            RegisterAsyncEventHandler<SubtaskCommented, TaskEventHandler>();
            RegisterAsyncEventHandler<SubtaskMarkedAsInProgress, TaskEventHandler>();
            RegisterAsyncEventHandler<SubtaskMarkedAsDone, TaskEventHandler>();

            //Nfr
            RegisterAsyncEventHandler<NfrCreated, NfrEventHandler>();
            RegisterAsyncEventHandler<NfrAssignedToSprint, NfrEventHandler>();
            RegisterAsyncEventHandler<AssigneeAssignedToNfr, NfrEventHandler>();
            RegisterAsyncEventHandler<LabelAssignedToNfr, NfrEventHandler>();
            RegisterAsyncEventHandler<NfrCommented, NfrEventHandler>();
            RegisterAsyncEventHandler<NfrMarkedAsInProgress, NfrEventHandler>();
            RegisterAsyncEventHandler<NfrMarkedAsDone, NfrEventHandler>();
            RegisterAsyncEventHandler<BugAddedToNfr, NfrEventHandler>();

            //Bug
            RegisterAsyncEventHandler<BugCreated, BugEventHandler>();
            RegisterAsyncEventHandler<BugAssignedToSprint, BugEventHandler>();
            RegisterAsyncEventHandler<AssigneeAssignedToBug, BugEventHandler>();
            RegisterAsyncEventHandler<LabelAssignedToBug, BugEventHandler>();
            RegisterAsyncEventHandler<BugCommented, BugEventHandler>();
            RegisterAsyncEventHandler<BugMarkedAsInProgress, BugEventHandler>();
            RegisterAsyncEventHandler<BugMarkedAsDone, BugEventHandler>();
        }

        public override void RegisterQueryHandlers()
        {
            //throw new NotImplementedException();
        }

        public override void AddAssemblyToProvider()
        {
            AssembliesProvider.assemblies.Add(typeof(ProjectManagementViewsBootstrap).GetTypeInfo().Assembly);
        }
    }
}
