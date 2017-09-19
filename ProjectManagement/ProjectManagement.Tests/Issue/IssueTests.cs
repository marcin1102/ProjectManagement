using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Infrastructure.Message.CommandQueryBus;
using ProjectManagement.Contracts.Issue.Enums;
using ProjectManagement.Contracts.Issue.Queries;
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
                IssueId = createIssue.CreatedId
            };
            var response = await commandQueryBus.SendAsync(getIssue);

            //Assert
            Assert.Equal(createIssue.CreatedId, response.Id);
        }
    }
}
