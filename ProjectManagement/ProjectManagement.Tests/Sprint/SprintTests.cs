using Autofac;
using ProjectManagement.Infrastructure.Message.CommandQueryBus;
using ProjectManagement.Contracts.DomainExceptions;
using ProjectManagement.Contracts.Issue.Commands;
using ProjectManagement.Contracts.Issue.Enums;
using ProjectManagement.Contracts.Sprint.Commands;
using ProjectManagement.Contracts.Sprint.Enums;
using ProjectManagement.Contracts.Sprint.Queries;
using ProjectManagement.Tests.Infrastructure;
using ProjectManagement.Tests.Issue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ProjectManagement.Tests.Sprint
{
    [Collection("FixtureCollection")]
    public class SprintTests
    {
        private readonly ProjectManagementFixture fixture;
        private readonly IComponentContext context;
        private readonly SeededData seededData;
        private readonly Random random;

        public SprintTests(ProjectManagementFixture fixture)
        {
            this.fixture = fixture;
            context = fixture.Module.Context;
            seededData = fixture.Module.SeededData;
            random = new Random();
        }

        // [Fact]
        // public async Task CreateSprint_SprintCreatedSuccesfully()
        // {
        //     //Arrange
        //     var name = SeededData.RandomString("SprintName_");
        //     var startDate = DateTime.UtcNow.Date;
        //     var endDate = DateTime.UtcNow.Date.AddDays(14);
        //     var createSprint = new CreateSprint(seededData.ProjectId, name, startDate, endDate);
        //     var commandQueryBus = context.Resolve<ICommandQueryBus>();

        //     //Act
        //     await commandQueryBus.SendAsync(createSprint);
        //     var id = createSprint.CreatedId;
        //     var response = await commandQueryBus.SendAsync(new GetSprint { Id = id });

        //     //Assert
        //     Assert.Equal(id, response.Id);
        //     Assert.Equal(seededData.ProjectId, response.ProjectId);
        //     Assert.Equal(name, response.Name);
        //     Assert.Equal(startDate, response.StartDate);
        //     Assert.Equal(endDate, response.EndDate);
        // }

        // [Fact]
        // public async Task StartSprint_SprintStarted()
        // {
        //     //Arrange
        //     var name = SeededData.RandomString("SprintName_");
        //     var startDate = DateTime.UtcNow.Date;
        //     var endDate = DateTime.UtcNow.Date.AddDays(14);
        //     var createSprint = new CreateSprint(seededData.ProjectId, name, startDate, endDate);
        //     var commandQueryBus = context.Resolve<ICommandQueryBus>();

        //     await commandQueryBus.SendAsync(createSprint);
        //     var id = createSprint.CreatedId;

        //     //Act
        //     await commandQueryBus.SendAsync(new StartSprint(id));

        //     //Assert
        //     var response = await commandQueryBus.SendAsync(new GetSprint { Id = id });

        //     Assert.Equal(SprintStatus.InProgress, response.Status);
        // }

        // [Fact]
        // public async Task FinishSprint_IncorrectStatus_CannotChangeSprintStatusThrown()
        // {
        //     //Arrange
        //     var name = SeededData.RandomString("SprintName_");
        //     var startDate = DateTime.UtcNow.Date;
        //     var endDate = DateTime.UtcNow.Date.AddDays(14);
        //     var createSprint = new CreateSprint(seededData.ProjectId, name, startDate, endDate);
        //     var commandQueryBus = context.Resolve<ICommandQueryBus>();

        //     await commandQueryBus.SendAsync(createSprint);
        //     var id = createSprint.CreatedId;

        //     //Assert
        //     await Assert.ThrowsAsync<CannotChangeSprintStatus>(() => commandQueryBus.SendAsync(new FinishSprint(id)));
        // }

        // [Fact]
        // public async Task FinishSprint_TwoOutOfThreeTasksAreDone_SprintFinishedSuccesfuly()
        // {
        //     //Arrange
        //     var name = SeededData.RandomString("SprintName_");
        //     var startDate = DateTime.UtcNow.Date;
        //     var endDate = DateTime.UtcNow.Date.AddDays(14);
        //     var createSprint = new CreateSprint(seededData.ProjectId, name, startDate, endDate);
        //     var commandQueryBus = context.Resolve<ICommandQueryBus>();

        //     await commandQueryBus.SendAsync(createSprint);
        //     var sprintId = createSprint.CreatedId;

        //     var issuesIds = await GenerateAndAssignThreeIssues(sprintId, commandQueryBus);

        //     await commandQueryBus.SendAsync(new StartSprint(sprintId));

        //     var unfinishedIssuesIds = await ChangeIssuesStatuses(issuesIds, sprintId, commandQueryBus);

        //     //Act
        //     await commandQueryBus.SendAsync(new FinishSprint(sprintId));

        //     //Assert
        //     var response = await commandQueryBus.SendAsync(new GetSprint { Id = sprintId});

        //     Assert.Equal(SprintStatus.Finished, response.Status);
        // }

        // private async Task<List<Guid>> ChangeIssuesStatuses(List<Guid> issuesIds, Guid id, ICommandQueryBus commandQueryBus)
        // {
        //     for (int i = 0; i < (issuesIds.Count-1); i++)
        //     {
        //         await commandQueryBus.SendAsync(new MarkAsInProgress(issuesIds.ElementAt(i), seededData.UserAssignedToProjectId));
        //     }

        //     var doneIssueId = issuesIds.First();
        //     await commandQueryBus.SendAsync(new MarkAsDone(doneIssueId, seededData.UserAssignedToProjectId));

        //     issuesIds.Remove(doneIssueId);
        //     return issuesIds;
        // }

        // private async Task<List<Guid>> GenerateAndAssignThreeIssues(Guid sprintId, ICommandQueryBus commandQueryBus)
        // {
        //     var commands = new List<ICreateIssue>();
        //     for (int i = 0; i < 3; i++)
        //     {
        //         commands.Add(IssueExtensions.GenerateBasicCreateIssueCommand(seededData, IssueType.Task));
        //     }

        //     foreach (var command in commands)
        //     {
        //         await commandQueryBus.SendAsync(command);
        //         await commandQueryBus.SendAsync(new AssignIssueToSprint(command.CreatedId, sprintId));
        //     }

        //     return commands.Select(x => x.CreatedId).ToList();
        // }
    }
}
