using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Exceptions;

namespace ProjectManagement.Contracts.DomainExceptions
{
    public class UserIsNotProjectMember : DomainException
    {
        public UserIsNotProjectMember(Guid userId, Guid projectId) :
            base("ProjectManagement", $"User with id {userId} is not assigned to the project with id {projectId}")
        {
        }
    }
}
