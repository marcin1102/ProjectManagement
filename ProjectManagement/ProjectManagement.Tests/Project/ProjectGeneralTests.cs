using System;
using System.Threading.Tasks;
using Autofac;
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
        private readonly ICommandQueryBus commandQueryBus;
        private readonly IEventManager eventManager;
        private readonly IComponentContext context;
        private readonly SeededData seededData;
        private readonly Random random;

        public ProjectGeneralTests(ProjectManagementFixture fixture)
        {
            this.fixture = fixture;
            context = fixture.Module.Context;
            commandQueryBus = context.Resolve<ICommandQueryBus>();
            eventManager = context.Resolve<IEventManager>();
            seededData = fixture.Module.SeededData;
            random = new Random();
        }

        [Fact]
        public async Task CreateNewProject_SuccesfullyCreated()
        {
            //Arrange
            var projectName = "TEST_PROJECT_" + random.Next(100000, 999999).ToString();
            var createProject = new CreateProject(seededData.AdminId, projectName);

            //Act
            await commandQueryBus.SendAsync(createProject);

            var getProject = new GetProject(createProject.CreatedId);
            var createdProject = await commandQueryBus.SendAsync(getProject);

            //Assert
            Assert.NotNull(createProject);
            Assert.Equal(projectName, createProject.Name);
        }
    }
}
