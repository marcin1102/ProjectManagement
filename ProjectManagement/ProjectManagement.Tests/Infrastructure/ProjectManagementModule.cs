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
        public ProjectManagementModule(IServiceProvider serviceProvider)
        {
            //CommandQueryBus = commandQueryBus;
            //EventManager = eventManager;
            ServiceProvider = serviceProvider;
            SeededData = new SeededData();
            SeededData.SeedData((ICommandQueryBus)serviceProvider.GetService(typeof(ICommandQueryBus)), (IEventManager)serviceProvider.GetService(typeof(IEventManager)));
        }

        public IServiceProvider ServiceProvider { get; private set; }
        //public ICommandQueryBus CommandQueryBus { get; private set; }
        //public IEventManager EventManager { get; private set; }
        public SeededData SeededData { get; private set; }

    }
}
