using System.Threading.Tasks;
using ProjectManagement.Infrastructure.Primitives.Exceptions;
using ProjectManagement.Infrastructure.Message.Handlers;
using UserManagement.Contracts.User.Queries;
using UserManagement.User.Repository;
using ProjectManagement.Infrastructure.CallContexts;
using UserManagement.UserView.Repository;
using System;

namespace UserManagement.UserView.Handlers
{
    public class UserQueryHandler : IAsyncQueryHandler<GetUser, UserResponse>
    {
        private readonly UserViewRepository repository;
        private readonly CallContext callContext;

        public UserQueryHandler(UserViewRepository repository, CallContext callContext)
        {
            this.repository = repository;
            this.callContext = callContext;
        }

        public async Task<UserResponse> HandleAsync(GetUser query)
        {
            var currentUserId = callContext.UserId;
            var currentUser = await repository.GetAsync(currentUserId);
            Model.UserView user;
            if (query.Id == Guid.Empty)
                user = await repository.GetAsync(query.Email);
            else
                user = await repository.GetAsync(query.Id);

            if (currentUser.Role != Contracts.User.Enums.Role.Admin && currentUserId != user.Id)
                throw new NotAuthorized(currentUserId, nameof(GetUser));

            return new UserResponse(user.Id, user.FirstName, user.LastName, user.Email, user.Role.ToString(), user.Version);
        }
    }
}
