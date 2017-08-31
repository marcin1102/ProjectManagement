using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Infrastructure.Message.Handlers;
using Infrastructure.Providers;
using Microsoft.Extensions.PlatformAbstractions;

namespace Infrastructure.Message.EventDispatcher
{
    public class DomainEventDispatcher : IDomainEventDispatcher
    {
        private readonly IEventBroker eventBroker;

        public DomainEventDispatcher(IEventBroker eventBroker)
        {
            this.eventBroker = eventBroker;
        }

        public async Task Dispatch<TEvent>(TEvent @event)
            where TEvent : IDomainEvent
        {
            var assembliesNames = Assembly.GetEntryAssembly().GetReferencedAssemblies();
            var projectsNames = ProjectsNamesProvider.GetDomainProjectsNames();
            var assemblies = assembliesNames.Where(assemblyName => projectsNames.Any(projectName => assemblyName.Name == projectName))
                .Select(x => Assembly.Load(x));


            foreach (var assembly in assemblies)
            {
                await eventBroker.DeliverEventTo(@event, assembly);
            }
        }
    }
}
