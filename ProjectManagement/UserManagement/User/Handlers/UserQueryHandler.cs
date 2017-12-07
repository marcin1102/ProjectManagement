using System.Threading.Tasks;
using Infrastructure.Exceptions;
using Infrastructure.Message.Handlers;
using UserManagement.Contracts.User.Queries;
using UserManagement.User.Repository;
using Infrastructure.CallContexts;

namespace UserManagement.User.Handlers
{
    public class UserQueryHandler : IAsyncQueryHandler<GetUser, UserResponse>
    {
        private readonly UserRepository repository;
        private readonly CallContext callContext;

        public UserQueryHandler(UserRepository repository, CallContext callContext)
        {
            this.repository = repository;
            this.callContext = callContext;
        }

        public async Task<UserResponse> HandleAsync(GetUser query)
        {
            var user = await repository.FindAsync(query.Id);

            return new UserResponse(user.Id, user.FirstName, user.LastName, user.Email, user.Role, user.Version);
        }
    }
}
