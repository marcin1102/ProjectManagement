using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Storage.EF.Repository;
using Microsoft.EntityFrameworkCore;

namespace ProjectManagement.User.Repository
{
    public class UserRepository : Repository<Model.User>
    {
        public UserRepository(ProjectManagementContext dbContext) : base(dbContext)
        {
        }
    }
}
