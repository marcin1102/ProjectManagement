using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Storage.EF;
using Microsoft.EntityFrameworkCore;
using ProjectManagementView.Storage.Models;

namespace ProjectManagementView
{
    public class ProjectManagementViewContext : BaseDbContext
    {
        public ProjectManagementViewContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<Sprint> Sprints { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Subtask> Subtasks { get; set; }
        public DbSet<Nfr> Nfrs { get; set; }
        public DbSet<Bug> Bugs { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>(x =>
            {
                x.HasKey(y => y.Id);
                x.HasMany(y => y.Sprints).WithOne();
                x.HasMany(y => y.Users);
            });

            modelBuilder.Entity<Sprint>(x =>
            {
                x.HasKey(y => y.Id);
                x.HasMany(y => y.Tasks).WithOne();
                x.HasMany(y => y.Nfrs).WithOne();
                x.HasMany(y => y.Bugs).WithOne();
                x.HasMany(y => y.Subtasks).WithOne();
            });

            modelBuilder.Entity<Task>(x =>
            {
                x.HasKey(y => y.Id);
                x.HasMany(y => y.Subtasks).WithOne();
                x.HasMany(y => y.Bugs).WithOne();
            });

            modelBuilder.Entity<Nfr>(x =>
            {
                x.HasKey(y => y.Id);
                x.HasMany(y => y.Bugs).WithOne();
            });

            modelBuilder.Entity<Bug>(x =>
            {
                x.HasKey(y => y.Id);
            });

            modelBuilder.Entity<Subtask>(x =>
            {
                x.HasKey(y => y.Id);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
