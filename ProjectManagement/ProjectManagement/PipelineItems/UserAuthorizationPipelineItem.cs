using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Exceptions;
using Infrastructure.Message;
using Infrastructure.Message.Pipeline.PipelineItems;
using ProjectManagement.Contracts.Interfaces;
using ProjectManagement.User.Repository;
using UserManagement.Contracts.User.Enums;

namespace ProjectManagement.PipelineItems
{
    public class UserAuthorizationPipelineItem<TCommand> : CommandPipelineItem<TCommand>
        where TCommand : IAuthorizationRequiredCommand
    {
        private readonly UserRepository userRepository;

        public UserAuthorizationPipelineItem(UserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public override async Task HandleAsync(TCommand command)
        {
            var user = await userRepository.FindAsync(command.UserId);

            if (user == null || user.Role != Role.Admin.ToString())
                throw new NotAuthorized(command.UserId, command.GetType().Name);

            await base.HandleAsync(command);
        }
    }
}
