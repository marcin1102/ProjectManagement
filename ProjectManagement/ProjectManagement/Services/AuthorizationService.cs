using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Exceptions;
using ProjectManagement.Contracts.DomainExceptions;
using ProjectManagement.Project.Searchers;
using ProjectManagement.User.Repository;
using UserManagement.Contracts.User.Enums;

namespace ProjectManagement.Services
{
    public interface IAuthorizationService
    {
        Task CheckUserMembership(Guid userId, Guid projectId);
        Task CheckUserRole(Guid userId, string commandName);
    }
    public class AuthorizationService : IAuthorizationService
    {
        private readonly UserRepository userRepository;
        private readonly IProjectSearcher projectSearcher;

        public AuthorizationService(UserRepository userRepository, IProjectSearcher projectSearcher)
        {
            this.userRepository = userRepository;
            this.projectSearcher = projectSearcher;
        }

        public async Task CheckUserMembership(Guid userId, Guid projectId)
        {
            var isUserProjectMember = await projectSearcher.IsUserProjectMember(projectId, userId);
            if (!isUserProjectMember)
                throw new UserIsNotProjectMember(userId, projectId);
        }

        public async Task CheckUserRole(Guid userId, string commandName)
        {
            var user = await userRepository.GetAsync(userId);

            if (user.Role != Role.Admin)
                throw new NotAuthorized(userId, commandName);
        }
    }
}
