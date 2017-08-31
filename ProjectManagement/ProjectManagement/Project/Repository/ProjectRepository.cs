using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Message.EventDispatcher;
using Infrastructure.Storage.EF.Repository;
using Microsoft.EntityFrameworkCore;

namespace ProjectManagement.Project.Repository
{
    public class ProjectRepository : AggregateRepository<Model.Project>
    {
        private readonly ProjectManagementContext context;

        public ProjectRepository(ProjectManagementContext context, IEventManager eventManager) : base(context, eventManager)
        {
            this.context = context;
        }
    }
}
