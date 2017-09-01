using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Storage.EF;

namespace UserManagement
{
    public class UserManagementContextFactory : DbContextFactory<UserManagementContext>
    {
    }
}
