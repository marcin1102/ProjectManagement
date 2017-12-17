using ProjectManagement.Infrastructure.Primitives.Message;
using System.Threading.Tasks;

namespace ProjectManagement.Infrastructure.Message.CommandQueryBus
{
    public interface ICommandQueryBus
    {
        Task SendAsync(ICommand command);
        Task<TResponse> SendAsync<TResponse>(IQuery<TResponse> query);
    }
}
