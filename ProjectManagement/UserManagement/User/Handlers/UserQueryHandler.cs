using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            var user = await repository.GetAsync(query.Id);

            return new UserResponse(user.Id, user.FirstName, user.LastName, user.Email, user.Role, user.Version);
        }
    }
}
