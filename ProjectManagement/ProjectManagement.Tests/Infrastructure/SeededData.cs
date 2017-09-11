﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Message;
using Infrastructure.Message.CommandQueryBus;
using Infrastructure.Message.EventDispatcher;
using ProjectManagement.Contracts.Project.Commands;
using UserManagement.Contracts.User.Enums;
using UserManagement.Contracts.User.Events;

namespace ProjectManagement.Tests.Infrastructure
{
    public class SeededData
    {
        public Guid AdminId { get; private set; }
        public Guid UserAssignedToProjectId { get; private set; }
        public Guid UserNotAssignedToProjectId { get; private set; }
        public Guid ProjectId { get; private set; }
        public const string ProjectName = "TEST_PROJECT";

        public SeededData()
        {
            AdminId = Guid.NewGuid();
            UserAssignedToProjectId = Guid.NewGuid();
            UserNotAssignedToProjectId = Guid.NewGuid();
        }

        public void SeedData(ICommandQueryBus commandQueryBus, IEventManager eventManager)
        {
            var queue = new Queue<IDomainEvent>();
            var adminCreated = new UserCreated(AdminId, "Admin", "Admin", "Admin@admin.pl", Role.Admin);
            var userAssignedCreated = new UserCreated(UserAssignedToProjectId, "Assigned", "Assigned", "Assigned@user.pl", Role.User);
            var userNotAssignedCreated = new UserCreated(UserNotAssignedToProjectId, "NotAssigned", "NotAssigned", "NotAssigned@user.pl", Role.User);
            queue.Enqueue(adminCreated);
            queue.Enqueue(userAssignedCreated);
            queue.Enqueue(userNotAssignedCreated);

            var createProject = new CreateProject(AdminId, ProjectName);

            Task.Run(() => eventManager.PublishEventsAsync(queue)).Wait();

            Task.Run(() => commandQueryBus.SendAsync(createProject)).Wait();
            ProjectId = createProject.CreatedId;

            Task.Run(() => commandQueryBus.SendAsync(new AssignUserToProject(AdminId, ProjectId, UserAssignedToProjectId, 1))).Wait();
        }
    }
}
