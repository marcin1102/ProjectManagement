using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Infrastructure.Message.CommandQueryBus;
using Infrastructure.Message.EventDispatcher;
using ProjectManagement.Contracts.Label.Commands;
using ProjectManagement.Contracts.Label.Queries;
using ProjectManagement.Tests.Infrastructure;
using Xunit;

namespace ProjectManagement.Tests.Label
{
    [Collection("FixtureCollection")]
    public class LabelTests
    {
        private readonly ProjectManagementFixture fixture;
        private readonly IComponentContext context;
        private readonly SeededData seededData;
        private readonly Random random;

        public LabelTests(ProjectManagementFixture fixture)
        {
            this.fixture = fixture;
            context = fixture.Module.Context;
            seededData = fixture.Module.SeededData;
            random = new Random();
        }

        [Fact]
        public async Task CreateLabel_CorrectData_LabelCreated()
        {
            //Arrange
            var labelName = "TEST_NAME" + random.Next(100000, 999999).ToString();
            var labelDesc = "TEST_DESC" + random.Next(100000, 999999).ToString();
            var createLabel = new CreateLabel(seededData.ProjectId, labelName, labelDesc);
            var commandQueryBus = context.Resolve<ICommandQueryBus>();

            //Act
            await commandQueryBus.SendAsync(createLabel);

            //Assert
            var result = await commandQueryBus.SendAsync(new GetLabel { Id = createLabel.CreatedId});
            Assert.Equal(createLabel.CreatedId, result.Id);
            Assert.Equal(seededData.ProjectId, result.ProjectId);
            Assert.Equal(labelName, result.Name);
            Assert.Equal(labelDesc, result.Description);
        }

        [Fact]
        public async Task GetLabelsPerProject_ProjectAndLabelsExistsInSystem_LabelsReturned()
        {
            //Arrange
            var labelsCount = 5;
            var commands = GenerateCreateLabelCommands(labelsCount);
            var commandQueryBus = context.Resolve<ICommandQueryBus>();
            foreach (var command in commands)
            {
                await commandQueryBus.SendAsync(command);
            }

            var projectId = seededData.ProjectId;
            var getLabels = new GetLabels
            {
                ProjectId = projectId
            };
            //Act
            var response = await commandQueryBus.SendAsync(getLabels);

            //Assert
            var assertValues = response.Select(entity => commands.Any(command => command.CreatedId == entity.Id));
            foreach (var assertValue in assertValues)
            {
                Assert.True(assertValue, "One of returned labels is incorrect(never created probably)");
            }
        }

        private ICollection<CreateLabel> GenerateCreateLabelCommands(int labelsCount)
        {
            var commands = new List<CreateLabel>();
            for (int i = 0; i < labelsCount; i++)
            {
                var labelName = "TEST_NAME" + random.Next(100000, 999999).ToString();
                var labelDesc = "TEST_DESC" + random.Next(100000, 999999).ToString();
                commands.Add(new CreateLabel(seededData.ProjectId, labelName, labelDesc));
            }
            return commands;
        }
    }
}
