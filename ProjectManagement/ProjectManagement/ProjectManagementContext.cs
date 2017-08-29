using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Storage.EF;
using Microsoft.EntityFrameworkCore;

namespace ProjectManagement
{
    public class ProjectManagementContext : BaseDbContext
    {
        public ProjectManagementContext(DbContextOptions<ProjectManagementContext> options, string schema) : base(options, schema)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(schema);
            base.OnModelCreating(modelBuilder);
        }
    }
}
