using System;
using System.Collections.Generic;
using System.Text;
using ProjectManagement.Infrastructure.Storage.EF;
using Microsoft.EntityFrameworkCore;
using ProjectManagementView.Storage.Models;
using ProjectManagementView.Storage.Models.Abstract;

namespace ProjectManagementView
{
    public class ProjectManagementViewContext : BaseDbContext
    {
        private const string SCHEMA = "project-management-views";
        public ProjectManagementViewContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<Sprint> Sprints { get; set; }
        public DbSet<Issue> Issues { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Subtask> Subtasks { get; set; }
        public DbSet<Nfr> Nfrs { get; set; }
        public DbSet<Bug> Bugs { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Label> Labels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>(x =>
            {
                x.HasKey(y => y.Id);
                x.HasMany(y => y.Sprints).WithOne();
                x.HasMany(y => y.Users);
                x.HasMany(y => y.Labels).WithOne();
            });

            modelBuilder.Entity<Sprint>(x =>
            {
                x.HasKey(y => y.Id);
                x.HasMany(y => y.Tasks).WithOne();
                x.HasMany(y => y.Nfrs).WithOne();
                x.HasMany(y => y.Bugs).WithOne();
                x.HasMany(y => y.Subtasks).WithOne();
                x.Ignore(y => y.UnfinishedIssues);
            });

            modelBuilder.Entity<Issue>(x =>
            {
                x.HasKey(y => y.Id);
                x.Property(y => y.ProjectId);
                x.Property(y => y.SprintId);
                x.Property("labels");
                x.Property("comments");
                x.Ignore(y => y.Labels);
                x.Ignore(y => y.Comments);
            });

            modelBuilder.Entity<Task>(x =>
            {
                x.HasMany(y => y.Subtasks).WithOne();
                x.HasMany(y => y.Bugs).WithOne();
            });

            modelBuilder.Entity<Nfr>(x =>
            {
                x.HasMany(y => y.Bugs).WithOne();
            });

            modelBuilder.Entity<Bug>();
            modelBuilder.Entity<Subtask>();

            modelBuilder.HasDefaultSchema(SCHEMA);
            base.OnModelCreating(modelBuilder);
        }
    }
}
