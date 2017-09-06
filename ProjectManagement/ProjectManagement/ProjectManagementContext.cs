using Infrastructure.Storage.EF;
using Microsoft.EntityFrameworkCore;

namespace ProjectManagement
{
    public class ProjectManagementContext : BaseDbContext
    {
        private const string SCHEMA = "project-management";

        public ProjectManagementContext(DbContextOptions<ProjectManagementContext> options) : base(options)
        {
        }

        public DbSet<Project.Model.Project> Projects { get; set; }
        public DbSet<User.Model.User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project.Model.Project>(x =>
            {
                x.HasKey(y => y.Id);
                x.Property(y => y.Name).IsRequired(true);
                x.Ignore(y => y.PendingEvents);
                x.ToTable(nameof(Project.Model.Project));
            });

            modelBuilder.Entity<User.Model.User>(x =>
            {
                x.HasKey(y => y.Id);
                x.Property(y => y.FirstName).IsRequired(true);
                x.Property(y => y.LastName).IsRequired(true);
                x.Property(y => y.Email).IsRequired(true);
                x.Property(y => y.Role).IsRequired(true);
                x.ToTable(nameof(User.Model.User));
            });

            modelBuilder.HasDefaultSchema(SCHEMA);
            base.OnModelCreating(modelBuilder);
        }
    }
}
