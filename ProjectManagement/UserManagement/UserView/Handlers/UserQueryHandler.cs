using System.Threading.Tasks;
using ProjectManagement.Infrastructure.Primitives.Exceptions;
using ProjectManagement.Infrastructure.Message.Handlers;
using UserManagement.Contracts.User.Queries;
using UserManagement.User.Repository;
using ProjectManagement.Infrastructure.CallContexts;
using UserManagement.UserView.Repository;
using System;
using System.Collections.Generic;
using UserManagement.UserView.Searchers;
using System.Linq;

namespace UserManagement.UserView.Handlers
{
    public class UserQueryHandler :
        IAsyncQueryHandler<GetUser, UserResponse>,
        IAsyncQueryHandler<GetUsers, IReadOnlyCollection<UserListItem>>
    {
        private readonly UserViewRepository repository;
        private readonly CallContext callContext;
        private readonly IUserViewSearcher userViewSearcher;

        public UserQueryHandler(UserViewRepository repository, CallContext callContext, IUserViewSearcher userViewSearcher)
        {
            this.repository = repository;
            this.callContext = callContext;
            this.userViewSearcher = userViewSearcher;
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

        public async Task<IReadOnlyCollection<UserListItem>> HandleAsync(GetUsers query)
        {
            var users = await userViewSearcher.GetUsers();
            return users.Select(x => new UserListItem(x.Id, x.FirstName, x.LastName, x.Role.ToString())).ToList();
        }
    }
}
