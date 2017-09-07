using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Exceptions.DomainExceptions;
using Infrastructure.Message.Handlers;
using UserManagement.Contracts.User.Queries;
using UserManagement.User.Repository;

namespace UserManagement.User.Handlers
{
    public class UserQueryHandler : IAsyncQueryHandler<GetUser, UserResponse>
    {
        private readonly UserRepository repository;

        public UserQueryHandler(UserRepository repository)
        {
            this.repository = repository;
        }

        public async Task<UserResponse> HandleAsync(GetUser query)
        {
            var user = await repository.FindAsync(query.Id);

            if (user == null)
                throw new EntityDoesNotExist(query.Id, nameof(Model.User));

            return new UserResponse(user.Id, user.FirstName, user.LastName, user.Email, user.Role, user.Version);
        }
    }
}
