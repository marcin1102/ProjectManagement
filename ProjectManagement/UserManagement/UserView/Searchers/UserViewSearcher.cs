using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.UserView.Model;

namespace UserManagement.UserView.Searchers
{
    public interface IUserViewSearcher
    {
        Task<IReadOnlyCollection<Model.UserView>> GetUsers();
    }

    public class UserViewSearcher : IUserViewSearcher
    {
        private UserManagementContext db;

        public UserViewSearcher(UserManagementContext db)
        {
            this.db = db;
        }

        public async Task<IReadOnlyCollection<Model.UserView>> GetUsers()
        {
            return await db.Users.Select(x => new Model.UserView(x.Id, x.FirstName, x.LastName, x.Email, x.Password, x.Role, x.Version)).ToListAsync();
        }
    }
}
