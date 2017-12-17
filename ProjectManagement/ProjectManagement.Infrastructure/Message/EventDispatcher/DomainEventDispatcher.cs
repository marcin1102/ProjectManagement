using System.Threading.Tasks;
using ProjectManagement.Infrastructure.Primitives.Message;
using ProjectManagement.Infrastructure.Providers;

namespace ProjectManagement.Infrastructure.Message.EventDispatcher
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
            //var assembliesNames = AssemblyNamesProvider.assemblyNames
            //var projectsNames = ProjectsNamesProvider.GetDomainProjectsNames();
            //var assemblies = AssembliesProvider.assemblies.Select(x => Assembly.Load(x)).ToList();

            foreach (var assembly in AssembliesProvider.assemblies)
            {
                await eventBroker.DeliverEventTo(@event, assembly);
            }
        }
    }
}
