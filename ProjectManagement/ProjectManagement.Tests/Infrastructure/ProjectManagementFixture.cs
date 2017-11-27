using System;
using Autofac;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace ProjectManagement.Tests.Infrastructure
{
    public class ProjectManagementFixture : IDisposable
    {
        public ProjectManagementModule Module { get; private set; }
        public ProjectManagementFixture(IServiceCollection services)
        {
            Module = new Bootstrap(
                services: services,
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
