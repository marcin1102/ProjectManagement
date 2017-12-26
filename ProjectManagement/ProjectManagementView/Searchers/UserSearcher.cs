using Microsoft.EntityFrameworkCore;
using ProjectManagementView.Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementView.Searchers
{
    public interface IUserSearcher
    {
        Task<IDictionary<Guid, string>> GetUsers(IReadOnlyCollection<Guid> userIds);
    }

    public class UserSearcher : IUserSearcher
    {
        private readonly ProjectManagementViewContext db;

        public UserSearcher(ProjectManagementViewContext db)
        {
            this.db = db;
        }

        public async Task<IDictionary<Guid, string>> GetUsers(IReadOnlyCollection<Guid> userIds)
        {
            var response = new Dictionary<Guid, string>();
            var users = await db.Users.Where(x => userIds.Any(y => x.Id == y)).ToListAsync();
            foreach (var user in users)
            {
                response.Add(user.Id, $"{user.FirstName} {user.LastName}");
            }

            return response;
        }
    }
}
