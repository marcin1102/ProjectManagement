using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Infrastructure.Message.CommandQueryBus;
using Infrastructure.Message.EventDispatcher;

namespace ProjectManagement.Tests.Infrastructure
{
    public class ProjectManagementModule
    {
        public ProjectManagementModule(IComponentContext context)
        {
            //CommandQueryBus = commandQueryBus;
            //EventManager = eventManager;
            Context = context;
            SeededData = new SeededData();
            SeededData.SeedData(context.Resolve<ICommandQueryBus>(), context.Resolve<IEventManager>());
        }

        public IComponentContext Context { get; private set; }
        //public ICommandQueryBus CommandQueryBus { get; private set; }
        //public IEventManager EventManager { get; private set; }
        public SeededData SeededData { get; private set; }

    }
}
