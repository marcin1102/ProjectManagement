using System;
using System.Collections.Generic;
using System.Text;
using ProjectManagement.Infrastructure.Storage.EF;

namespace UserManagement
{
    public class UserManagementContextFactory : DbContextFactory<UserManagementContext>
    {
    }
}
