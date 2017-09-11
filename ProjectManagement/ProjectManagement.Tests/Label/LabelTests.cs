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
            var result = await commandQueryBus.SendAsync(new GetLabel(createLabel.CreatedId));
            Assert.Equal(createLabel.CreatedId, result.Id);
            Assert.Equal(seededData.ProjectId, result.ProjectId);
            Assert.Equal(labelName, result.Name);
            Assert.Equal(labelDesc, result.Description);
        }
    }
}
