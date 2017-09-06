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

        public DbSet<User.Model.User> Users { get; set; }

        public UserManagementContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User.Model.User>(x =>
            {
                x.HasKey(y => y.Id);
                x.HasAlternateKey(y => y.Email);
                x.Property(y => y.FirstName).IsRequired();
                x.Property(y => y.LastName).IsRequired();
                x.Property(y => y.Role).IsRequired();
                x.Ignore(y => y.PendingEvents);
                x.ToTable(nameof(User.Model.User));
            });

            modelBuilder.HasDefaultSchema(SCHEMA);
            base.OnModelCreating(modelBuilder);
        }
    }
}
