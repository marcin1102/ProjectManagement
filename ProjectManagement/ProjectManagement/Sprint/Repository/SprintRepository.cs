using ProjectManagement.Infrastructure.Storage.EF.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using ProjectManagement.Infrastructure.Message.EventDispatcher;
using Microsoft.EntityFrameworkCore;

namespace ProjectManagement.Sprint.Repository
{
    public class SprintRepository : AggregateRepository<Model.Sprint>
    {
        public SprintRepository(ProjectManagementContext dbContext, IEventManager eventManager) : base(dbContext, eventManager)
        {
        }
    }
}
