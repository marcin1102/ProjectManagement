using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Infrastructure.Bootstrap;
using ProjectManagement.Contracts;

namespace UserManagement
{
    public class UserManagementBootstrap : ModuleBootstrap
    {
        public UserManagementBootstrap(ContainerBuilder builder) : base(builder)
        {
        }

        public override void RegisterCommandHandlers()
        {
        }

        public override void RegisterEventHandlers()
        {
            RegisterAsyncEventHandler<TestDomainEvent, TestEventHandler>();
        }

        public override void RegisterQueryHandlers()
        {
        }
    }
}
