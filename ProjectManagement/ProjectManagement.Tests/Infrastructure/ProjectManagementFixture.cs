using System;
using Autofac;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ProjectManagement.Tests.Infrastructure
{
    public class ProjectManagementFixture : IDisposable
    {
        public ProjectManagementModule Module { get; private set; }
        public ProjectManagementFixture()
        {
            Module = new Bootstrap(
                builder: new ContainerBuilder(),
                configuration: new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json")
                    .AddEnvironmentVariables()
                    .Build(),
                logger: new LoggerFactory()
            ).Build();
        }

        public void Dispose()
        {
        }
    }
}
