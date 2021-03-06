﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManagement.Infrastructure.Message.EventDispatcher;
using ProjectManagement.Infrastructure.Storage.EF.Repository;
using Microsoft.EntityFrameworkCore;

namespace UserManagement.User.Repository
{
    public class UserRepository : AggregateRepository<Model.User>
    {
        public UserRepository(UserManagementContext dbContext, IEventManager eventManager) : base(dbContext, eventManager)
        {
        }
    }
}
