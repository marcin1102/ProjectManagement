using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Message;
using Infrastructure.Message.CommandQueryBus;
using Infrastructure.Message.EventDispatcher;
using ProjectManagement.Contracts.Issue.Enums;
using ProjectManagement.Contracts.Label.Commands;
using ProjectManagement.Contracts.Project.Commands;
using ProjectManagement.Tests.Issue;
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
        public Guid TaskId { get; private set; }
        public Guid NfrId { get; private set; }
        public Guid BugId { get; private set; }
        public ICollection<Guid> LabelsIds { get; private set; }
        public const string ProjectName = "TEST_PROJECT";
        private static Random random = new Random();

        public SeededData()
        {
            AdminId = Guid.NewGuid();
            UserAssignedToProjectId = Guid.NewGuid();
            UserNotAssignedToProjectId = Guid.NewGuid();
            LabelsIds = new List<Guid>();
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

            CreateLabel createLabel;
            for (int i = 0; i < 5; i++)
            {
                createLabel = new CreateLabel(ProjectId, RandomString("NAME_"), RandomString("DESCR_"));
                Task.Run(() => commandQueryBus.SendAsync(createLabel)).Wait();
                LabelsIds.Add(createLabel.CreatedId);
            }


            var createIssue = IssueExtensions.GenerateBasicCreateIssueCommand(this, IssueType.Task);
            Task.Run(() => commandQueryBus.SendAsync(createIssue)).Wait();
            TaskId = createIssue.CreatedId;

            createIssue = IssueExtensions.GenerateBasicCreateIssueCommand(this, IssueType.Nfr);
            Task.Run(() => commandQueryBus.SendAsync(createIssue)).Wait();
            NfrId = createIssue.CreatedId;

            createIssue = IssueExtensions.GenerateBasicCreateIssueCommand(this, IssueType.Bug);
            Task.Run(() => commandQueryBus.SendAsync(createIssue)).Wait();
            BugId = createIssue.CreatedId;

        }

        public static string RandomString(string @base = null)
        {
            string randomString;
            randomString = @base != null ? @base : "random";
            return randomString + random.Next(100000, 999999);
        }
    }
}
