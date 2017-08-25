using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Message.CommandQueryBus
{
    public class CommandQueryBusPipeline : ICommandQueryBus
    {
        public Task SendAsync(ICommand command)
        {
            throw new NotImplementedException();
        }

        public Task<TResponse> SendAsync<TResponse>(IQuery<TResponse> query)
        {
            throw new NotImplementedException();
        }
    }
}
