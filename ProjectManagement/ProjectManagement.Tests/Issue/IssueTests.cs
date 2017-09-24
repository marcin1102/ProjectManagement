using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Infrastructure.Message.CommandQueryBus;
using ProjectManagement.Contracts.DomainExceptions;
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

        List<Guid> GetRandomElements(ICollection<Guid> collection) => collection.OrderBy(x => Guid.NewGuid()).Take(2).ToList();

        [Fact]
        public async Task CreateIssue_CorrectData_IssueCreated()
        {
            //Arrange
            var createIssue = IssueExtensions
                .GenerateBasicCreateIssueCommand(seededData, IssueType.Task)
                .WithLabels(GetRandomElements(seededData.LabelsIds))
                .WithSubtasks(new List<Guid> { seededData.BugId} )
                .WithAssignee(seededData.UserAssignedToProjectId);

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
            Assert.Equal(createIssue.LabelsIds.Count, response.LabelsIds.Count);
            Assert.Equal(createIssue.SubtasksIds.Count, response.SubtasksIds.Count);
            Assert.Equal(createIssue.AssigneeId, response.AssigneeId);
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

        [Fact]
        public async Task MarkIssueAsInProgress_CorrectData_IssueMarkedAsInProgress()
        {
            //Arrange
            var userId = seededData.UserAssignedToProjectId;
            var createIssue = IssueExtensions.GenerateBasicCreateIssueCommand(seededData, IssueType.Bug);

            var commandQueryBus = context.Resolve<ICommandQueryBus>();
            await commandQueryBus.SendAsync(createIssue);

            var issueId = createIssue.CreatedId;
            await commandQueryBus.SendAsync(new AssignIssueToSprint(issueId, seededData.SprintId));

            //Act
            var markAsInProgress = new MarkAsInProgress(issueId, userId);
            await commandQueryBus.SendAsync(markAsInProgress);

            //Assert
            var getIssue = new GetIssue { Id = issueId };
            var response = await commandQueryBus.SendAsync(getIssue);
            Assert.Equal(IssueStatus.InProgress, response.Status);
        }

        [Fact]
        public async Task MarkIssueAsInProgress_IssueIsInProgressAlready_CannotChangeIssueStatusThrown()
        {
            //Arrange
            var userId = seededData.UserAssignedToProjectId;
            var createIssue = IssueExtensions.GenerateBasicCreateIssueCommand(seededData, IssueType.Bug);

            var commandQueryBus = context.Resolve<ICommandQueryBus>();
            await commandQueryBus.SendAsync(createIssue);

            var issueId = createIssue.CreatedId;
            await commandQueryBus.SendAsync(new AssignIssueToSprint(issueId, seededData.SprintId));

            //Act
            var markAsInProgress = new MarkAsInProgress(issueId, userId);
            await commandQueryBus.SendAsync(markAsInProgress);

            //Assert
            await Assert.ThrowsAsync<CannotChangeIssueStatus>(async () => await commandQueryBus.SendAsync(markAsInProgress));
        }

        [Fact]
        public async Task MarkIssueAsDone_IssueIsMarkedAsToDo_CannotChangeIssueStatusThrown()
        {
            //Arrange
            var userId = seededData.UserAssignedToProjectId;
            var createIssue = IssueExtensions.GenerateBasicCreateIssueCommand(seededData, IssueType.Bug);
            var commandQueryBus = context.Resolve<ICommandQueryBus>();

            //Act
            await commandQueryBus.SendAsync(createIssue);
            var issueId = createIssue.CreatedId;
            await commandQueryBus.SendAsync(new AssignIssueToSprint(issueId, seededData.SprintId));

            //Assert
            await Assert.ThrowsAsync<CannotChangeIssueStatus>(async () => await commandQueryBus.SendAsync(new MarkAsDone(issueId, userId)));
        }

        [Fact]
        public async Task MarkIssueAsDone_AllCasesAreValid_IssueMarkedAsDone()
        {
            //Arrange
            var userId = seededData.UserAssignedToProjectId;
            var createBug = IssueExtensions.GenerateBasicCreateIssueCommand(seededData, IssueType.Bug);
            var commandQueryBus = context.Resolve<ICommandQueryBus>();

            await commandQueryBus.SendAsync(createBug);
            var bugId = createBug.CreatedId;

            var createIssue = IssueExtensions.GenerateBasicCreateIssueCommand(seededData, IssueType.Task).WithSubtasks(new List<Guid> { bugId });
            await commandQueryBus.SendAsync(createIssue);
            var issueId = createIssue.CreatedId;

            await commandQueryBus.SendAsync(new AssignIssueToSprint(issueId, seededData.SprintId));
            await commandQueryBus.SendAsync(new AssignIssueToSprint(bugId, seededData.SprintId));
            await commandQueryBus.SendAsync(new MarkAsInProgress(issueId, userId));
            await commandQueryBus.SendAsync(new MarkAsInProgress(bugId, userId));
            await commandQueryBus.SendAsync(new MarkAsDone(bugId, userId));

            //Act
            await commandQueryBus.SendAsync(new MarkAsDone(issueId, userId));

            //Assert
            var issue = await commandQueryBus.SendAsync(new GetIssue { Id = issueId });
            Assert.Equal(IssueStatus.Done, issue.Status);
        }

        [Fact]
        public async Task MarkIssueAsDone_IssueHasSubtasksThatHaveNotBeenMarkedAsDone_AllRelatedIssuesMustBeDoneThrown()
        {
            //Arrange
            var userId = seededData.UserAssignedToProjectId;
            var createBug = IssueExtensions.GenerateBasicCreateIssueCommand(seededData, IssueType.Bug);
            var commandQueryBus = context.Resolve<ICommandQueryBus>();

            //Act
            await commandQueryBus.SendAsync(createBug);
            var bugId = createBug.CreatedId;

            var createIssue = IssueExtensions.GenerateBasicCreateIssueCommand(seededData, IssueType.Task).WithSubtasks(new List<Guid> { bugId });
            await commandQueryBus.SendAsync(createIssue);
            var issueId = createIssue.CreatedId;
            await commandQueryBus.SendAsync(new AssignIssueToSprint(issueId, seededData.SprintId));
            await commandQueryBus.SendAsync(new MarkAsInProgress(issueId, userId));

            //Assert
            await Assert.ThrowsAsync<AllRelatedIssuesMustBeDone>(async () => await commandQueryBus.SendAsync(new MarkAsDone(issueId, userId)));
        }

        [Fact]
        public async Task AssignIssueToSprint_IssueAssignedToSprint()
        {
            //Arrange
            var userId = seededData.UserAssignedToProjectId;
            var createIssue = IssueExtensions.GenerateBasicCreateIssueCommand(seededData, IssueType.Task);
            var commandQueryBus = context.Resolve<ICommandQueryBus>();

            await commandQueryBus.SendAsync(createIssue);
            var issueId = createIssue.CreatedId;

            //Act
            await commandQueryBus.SendAsync(new AssignIssueToSprint(issueId, seededData.SprintId));

            //Assert
            var respone = await commandQueryBus.SendAsync(new GetIssue { Id = issueId });

            Assert.Equal(seededData.SprintId, respone.SprintId);
        }
    }
}
