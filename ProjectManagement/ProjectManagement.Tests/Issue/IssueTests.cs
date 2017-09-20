using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Infrastructure.Message.CommandQueryBus;
using ProjectManagement.Contracts.Exceptions;
using ProjectManagement.Contracts.Issue.Commands;
using ProjectManagement.Contracts.Issue.Enums;
using ProjectManagement.Contracts.Issue.Queries;
using ProjectManagement.Contracts.Label.Commands;
using ProjectManagement.Tests.Infrastructure;
using Xunit;

namespace ProjectManagement.Tests.Issue
{
    [Collection("FixtureCollection")]
    public class IssueTests
    {
        private readonly ProjectManagementFixture fixture;
        private readonly IComponentContext context;
        private readonly SeededData seededData;
        private readonly Random random;
        public IssueTests(ProjectManagementFixture fixture)
        {
            this.fixture = fixture;
            context = fixture.Module.Context;
            seededData = fixture.Module.SeededData;
            random = new Random();
        }

        [Fact]
        public async Task CreateIssue_BasicCorrectData_IssueCreated()
        {
            //Arrange
            var createIssue = IssueExtensions.GenerateBasicCreateIssueCommand(seededData, IssueType.Task);
            var commandQueryBus = context.Resolve<ICommandQueryBus>();

            //Act
            await commandQueryBus.SendAsync(createIssue);

            var getIssue = new GetIssue
            {
                Id = createIssue.CreatedId
            };
            var response = await commandQueryBus.SendAsync(getIssue);

            //Assert
            Assert.Equal(createIssue.CreatedId, response.Id);
        }

        [Fact]
        public async Task AssignLabelsToIssue_LabelsAssignedCorrectly()
        {
            //Arrange
            var createLabelCommands = new List<CreateLabel>();
            for (int i = 0; i < 3; i++)
            {
                createLabelCommands.Add(new CreateLabel(seededData.ProjectId, "LABEL" + random.Next(100000, 999999), "LABEL_DESC" + random.Next(100000, 999999)));
            }
            var commandQueryBus = context.Resolve<ICommandQueryBus>();
            foreach (var command in createLabelCommands)
            {
                await commandQueryBus.SendAsync(command);
            }
            var labelsIds = createLabelCommands.Select(x => x.CreatedId).ToList();
            var issueId = seededData.TaskId;

            //Act
            await commandQueryBus.SendAsync(new AssignLabelsToIssue(issueId, labelsIds));

            //Assert
            var issue = await commandQueryBus.SendAsync(new GetIssue
            {
                Id = issueId
            });

            Assert.Equal(labelsIds.Count, issue.LabelsIds.Count);
        }

        [Fact]
        public async Task AddSubtask_AddSubtaskToBug_CannotAddSubtaskToBugThrown()
        {
            var addTaskAsSubtask = new AddSubtask(seededData.BugId, seededData.TaskId);
            var addNfrkAsSubtask = new AddSubtask(seededData.BugId, seededData.NfrId);
            var commandQueryBus = context.Resolve<ICommandQueryBus>();


            await Assert.ThrowsAsync<CannotAddSubtaskToBug>(async () => await commandQueryBus.SendAsync(addTaskAsSubtask));
            await Assert.ThrowsAsync<CannotAddSubtaskToBug>(async () => await commandQueryBus.SendAsync(addNfrkAsSubtask));
        }

        [Fact]
        public async Task AddSubtask_AddSubtaskToNfr_NfrsCanHaveOnlyBugs()
        {
            var addTaskAsSubtask = new AddSubtask(seededData.NfrId, seededData.TaskId);
            var addBugkAsSubtask = new AddSubtask(seededData.NfrId, seededData.BugId);
            var commandQueryBus = context.Resolve<ICommandQueryBus>();


            await Assert.ThrowsAsync<NfrsCanHaveOnlyBugs>(async () => await commandQueryBus.SendAsync(addTaskAsSubtask));

            await commandQueryBus.SendAsync(addBugkAsSubtask);
            var response = await commandQueryBus.SendAsync(new GetIssue { Id = seededData.NfrId });
            var subtasks = response.SubtasksIds;
            Assert.Equal(1, subtasks.Count);
            Assert.Equal(seededData.BugId, response.SubtasksIds.SingleOrDefault());
        }

        [Fact]
        public async Task AddSubtask_AddSubtaskToTask_CannotAddNfrAsSubTask()
        {
            var addNfrAsSubtask = new AddSubtask(seededData.TaskId, seededData.NfrId);
            var addBugkAsSubtask = new AddSubtask(seededData.TaskId, seededData.BugId);
            var commandQueryBus = context.Resolve<ICommandQueryBus>();


            await Assert.ThrowsAsync<CannotAddNfrAsSubtask>(async () => await commandQueryBus.SendAsync(addNfrAsSubtask));

            await commandQueryBus.SendAsync(addBugkAsSubtask);
            var response = await commandQueryBus.SendAsync(new GetIssue { Id = seededData.TaskId });
            var subtasks = response.SubtasksIds;
            Assert.Equal(1, subtasks.Count);
            Assert.Equal(seededData.BugId, response.SubtasksIds.SingleOrDefault());
        }
    }
}
