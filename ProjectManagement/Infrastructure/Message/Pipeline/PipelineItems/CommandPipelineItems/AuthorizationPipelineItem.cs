using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Message.Pipeline.PipelineItems.CommandPipelineItems
{
    public class AuthorizationPipelineItem<TCommand> : CommandPipelineItem<TCommand>
        where TCommand : class, ICommand
    {
        
    }
}
