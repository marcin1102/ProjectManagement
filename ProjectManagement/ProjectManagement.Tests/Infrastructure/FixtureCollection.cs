using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ProjectManagement.Tests.Infrastructure
{
    [CollectionDefinition("FixtureCollection")]
    public class FixtureCollection : ICollectionFixture<ProjectManagementFixture>
    {
    }
}
