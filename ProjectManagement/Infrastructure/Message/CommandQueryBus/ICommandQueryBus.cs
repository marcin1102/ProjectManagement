using System.Threading.Tasks;

namespace Infrastructure.Message.CommandQueryBus
{
    public interface ICommandQueryBus
    {
        Task SendAsync(ICommand command);
        Task<TResponse> SendAsync<TResponse>(IQuery<TResponse> query);
    }
}
