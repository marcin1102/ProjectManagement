using Infrastructure.Message.Handlers;
using ProjectManagement.Contracts.User.Queries;
using ProjectManagement.User.Searchers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.User.Handlers
{
    public class UserQueryHandler :
        IAsyncQueryHandler<GetUnfinishedIssuesAssignees, ICollection<UserListItem>>
    {
        private readonly IUserSearcher userSearcher;

        public UserQueryHandler(IUserSearcher userSearcher)
        {
            this.userSearcher = userSearcher;
        }

        public async Task<ICollection<UserListItem>> HandleAsync(GetUnfinishedIssuesAssignees query)
        {
            var users = await userSearcher.GetUnfinishedIssuesAssignees(query.SprintId);
            return users.Select(x => new UserListItem(x.Id, x.FirstName, x.LastName, x.Email)).ToList();
        }
    }
}
