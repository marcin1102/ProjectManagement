using System;
using Infrastructure.Bootstrap;
using Microsoft.Extensions.DependencyInjection;
using Autofac;
using Infrastructure.Message.Handlers;
using ProjectManagement.Contracts;

namespace ProjectManagement
{
    public class ProjectManagementBootstrap : ModuleBootstrap
    {
        public ProjectManagementBootstrap(ContainerBuilder builder) : base(builder)
        {
        }

        public override void RegisterCommandHandlers()
        {
            RegisterAsyncCommandHandler<TestCommand, TestCommandHandler>();
        }

        public override void RegisterEventHandlers()
        {
            RegisterAsyncQueryHandler<TestQuery, TestResponse, TestQueryHandler>();
        }

        public override void RegisterQueryHandlers()
        {

        }
    }
}
