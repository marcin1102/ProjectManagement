﻿using System;
using Autofac;
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

        // [Fact]
        // public async Task CreateLabel_CorrectData_LabelCreated()
        // {
        //     //Arrange
        //     var labelName = "TEST_NAME" + random.Next(100000, 999999).ToString();
        //     var labelDesc = "TEST_DESC" + random.Next(100000, 999999).ToString();
        //     var createLabel = new AddLabel(labelName, labelDesc);
        //     var commandQueryBus = context.Resolve<ICommandQueryBus>();

        //     //Act
        //     await commandQueryBus.SendAsync(createLabel);

        //     //Assert
        //     var result = await commandQueryBus.SendAsync(new GetLabel { Id = createLabel.CreatedId});
        //     Assert.Equal(createLabel.CreatedId, result.Id);
        //     Assert.Equal(seededData.ProjectId, result.ProjectId);
        //     Assert.Equal(labelName, result.Name);
        //     Assert.Equal(labelDesc, result.Description);
        // }

        // [Fact]
        // public async Task GetLabelsPerProject_ProjectAndLabelsExistsInSystem_LabelsReturned()
        // {
        //     //Arrange
        //     var labelsCount = 5;
        //     var commands = GenerateCreateLabelCommands(labelsCount);
        //     var commandQueryBus = context.Resolve<ICommandQueryBus>();
        //     foreach (var command in commands)
        //     {
        //         await commandQueryBus.SendAsync(command);
        //     }

        //     var projectId = seededData.ProjectId;
        //     var getLabels = new GetLabels
        //     {
        //         ProjectId = projectId
        //     };
        //     var ids = commands.Select(x => x.CreatedId);
        //     //Act
        //     var response = await commandQueryBus.SendAsync(getLabels);

        //     //Assert
        //     var elementsCount = response.Select(x => x.Id).Where(x => ids.Contains(x)).Count();
        //     Assert.Equal(labelsCount, elementsCount);
        // }

        // private ICollection<AddLabel> GenerateCreateLabelCommands(int labelsCount)
        // {
        //     var commands = new List<AddLabel>();
        //     for (int i = 0; i < labelsCount; i++)
        //     {
        //         var labelName = "TEST_NAME" + random.Next(100000, 999999).ToString();
        //         var labelDesc = "TEST_DESC" + random.Next(100000, 999999).ToString();
        //         commands.Add(new AddLabel(seededData.ProjectId, labelName, labelDesc));
        //     }
        //     return commands;
        // }
    }
}
