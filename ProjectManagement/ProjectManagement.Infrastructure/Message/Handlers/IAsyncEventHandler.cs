using ProjectManagement.Infrastructure.Primitives.Message;
using System.Threading.Tasks;

namespace ProjectManagement.Infrastructure.Message.Handlers
{
    public interface IAsyncEventHandler<TEvent> where TEvent : IDomainEvent
    {
        Task HandleAsync(TEvent @event);
    }
}
