using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ProjectManagement.Infrastructure.Primitives.Exceptions;
using ProjectManagement.Contracts.DomainExceptions;
using ProjectManagement.Project.Searchers;
using ProjectManagement.User.Repository;
using UserManagement.Contracts.User.Enums;

namespace ProjectManagement.Services
{
    public interface IMembershipService
    {
        Task CheckUserMembership(Guid userId, Guid projectId);
    }
    public class MembershipService : IMembershipService
    {
        private readonly UserRepository userRepository;
        private readonly IProjectSearcher projectSearcher;

        public MembershipService(UserRepository userRepository, IProjectSearcher projectSearcher)
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
    }
}
