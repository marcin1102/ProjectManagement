using System;
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
        public Guid ProjectId { get; private set; }
        public const string ProjectName = "TEST_PROJECT";

        public SeededData()
        {
            AdminId = Guid.NewGuid();
            ProjectId = Guid.NewGuid();
        }

        public async void SeedData(ICommandQueryBus commandQueryBus, IEventManager eventManager)
        {
            var queue = new Queue<IDomainEvent>();
            var userCreated = new UserCreated(AdminId, "", "", "", Role.Admin);
            queue.Enqueue(userCreated);

            await eventManager.PublishEventsAsync(queue);

            await commandQueryBus.SendAsync(new CreateProject(AdminId, ProjectName));
        }
    }
}
