using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UserManagement.User.Model;

namespace UserManagement.User.Searchers
{
    public interface IUserSearcher
    {
        Task<Model.User> FindUser(string email);
    }

    public class UserSearcher : IUserSearcher
    {
        private readonly UserManagementContext context;

        public UserSearcher(UserManagementContext context)
        {
            this.context = context;
        }

        public Task<Model.User> FindUser(string email)
        {
            return context.Users.SingleOrDefaultAsync(x => x.Email == email);
        }
    }
}
