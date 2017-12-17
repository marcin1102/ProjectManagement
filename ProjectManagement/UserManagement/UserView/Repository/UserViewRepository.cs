using Microsoft.EntityFrameworkCore;
using ProjectManagement.Infrastructure.Primitives.Exceptions;
using ProjectManagement.Infrastructure.Storage.EF.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.UserView.Model;

namespace UserManagement.UserView.Repository
{
    public class UserViewRepository : Repository<Model.UserView>
    {
        public UserViewRepository(UserManagementContext dbContext) : base(dbContext)
        {
        }

        public async override Task<Model.UserView> GetAsync(Guid id)
        {
            if (dbContext is UserManagementContext db)
            {
                var user = await db.Users.SingleOrDefaultAsync(x => x.Id == id);
                if (user == null)
                    throw new EntityDoesNotExist(id, nameof(Model.UserView));
                return new Model.UserView(user.Id, user.FirstName, user.LastName, user.Email, user.Password, user.Role, user.Version);
            }
            else
                throw new Exception("Cannot map dbContext to UserManagementContext");
        }

        public async Task<Model.UserView> GetAsync(string email)
        {
            if (dbContext is UserManagementContext db)
            {
                var user = await db.Users.SingleOrDefaultAsync(x => x.Email == email);
                if (user == null)
                    throw new EntityDoesNotExist($"user with email {email} does not exist");

                return new Model.UserView(user.Id, user.FirstName, user.LastName, user.Email, user.Password, user.Role, user.Version);
            }
            else
                throw new Exception("Cannot map dbContext to UserManagementContext");
        }
    }
}
