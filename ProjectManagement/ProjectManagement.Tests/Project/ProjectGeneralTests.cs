using System;
using System.Threading.Tasks;
using Autofac;
using Infrastructure.Exceptions;
using Infrastructure.Message.CommandQueryBus;
using Infrastructure.Message.EventDispatcher;
using ProjectManagement.Contracts.Project.Commands;
using ProjectManagement.Contracts.Project.Queries;
using ProjectManagement.Tests.Infrastructure;
using Xunit;

namespace ProjectManagement.Tests.Project
{
    [Collection("FixtureCollection")]
    public class ProjectGeneralTests
    {
        private readonly ProjectManagementFixture fixture;
        private readonly IComponentContext context;
        private readonly SeededData seededData;
        private readonly Random random;

        public ProjectGeneralTests(ProjectManagementFixture fixture)
        {
            this.fixture = fixture;
            context = fixture.Module.Context;
            seededData = fixture.Module.SeededData;
            random = new Random();
        }

        // [Fact]
        // public async Task CreateNewProject_CreateProjectAsAdmin_SuccesfullyCreated()
        // {
        //     //Arrange
        //     var commandQueryBus = context.Resolve<ICommandQueryBus>();
        //     var projectName = "TEST_PROJECT_" + random.Next(100000, 999999).ToString();
        //     var createProject = new CreateProject(seededData.AdminId, projectName);

        //     //Act
        //     await commandQueryBus.SendAsync(createProject);

        //     var getProject = new GetProject
        //     {
        //         Id = createProject.CreatedId
        //     };
        //     var createdProject = await commandQueryBus.SendAsync(getProject);

        //     //Assert
        //     Assert.NotNull(createProject);
        //     Assert.Equal(projectName, createProject.Name);
        // }

        // [Fact]
        // public async Task CreateNewProject_CreateProjectAsUser_ProjectCreationFailed()
        // {
        //     //Arrange
        //     var commandQueryBus = context.Resolve<ICommandQueryBus>();
        //     var projectName = "TEST_PROJECT_" + random.Next(100000, 999999).ToString();

        //     //Act
        //     var createProject = new CreateProject(seededData.UserNotAssignedToProjectId, projectName);

        //     //Assert
        //     var exception = await Assert.ThrowsAsync<NotAuthorized>(async () => await commandQueryBus.SendAsync(createProject));
        //     Assert.Equal(seededData.UserNotAssignedToProjectId, exception.UserId);
        //     Assert.Equal(nameof(CreateProject), exception.CommandName);
        // }

        // [Fact]
        // public async Task GetProject_ProjectDoesNotExist_ExceptionThrown()
        // {
        //     //Arrange
        //     var id = Guid.NewGuid();
        //     var getProject = new GetProject
        //     {
        //         Id = id
        //     };
        //     var commandQueryBus = context.Resolve<ICommandQueryBus>();

        //     //Assert
        //     var exception = await Assert.ThrowsAsync<EntityDoesNotExist>(async () => await commandQueryBus.SendAsync(getProject));
        //     Assert.Equal(id, exception.EntityId);
        //     Assert.Equal("Project", exception.EntityName);
        // }
    }
}
