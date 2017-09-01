using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Storage.EF;
using Microsoft.EntityFrameworkCore;

namespace UserManagement
{
    public class UserManagementContext : BaseDbContext
    {
        private const string SCHEMA = "user-management";

        public UserManagementContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(SCHEMA);
            base.OnModelCreating(modelBuilder);
        }
    }
}
