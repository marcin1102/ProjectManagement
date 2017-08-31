using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Storage.EF;
using Infrastructure.Storage.EF.EventContext;

namespace ProjectManagement
{
    public class ProjectManagementContextFactory : DbContextFactory<EventContext>
    {
    }
}
