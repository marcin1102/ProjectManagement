﻿using System;
using System.Collections.Generic;
using System.Text;
using ProjectManagement.Infrastructure.Storage.EF;

namespace ProjectManagement
{
    public class ProjectManagementContextFactory : DbContextFactory<ProjectManagementContext>
    {
    }
}
