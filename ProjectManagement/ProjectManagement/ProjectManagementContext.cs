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
        public DbSet<ProjectUser.ProjectUser> ProjectsUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project.Model.Project>(x =>
            {
                x.HasKey(y => y.Id);
                x.Property(y => y.Id).ValueGeneratedNever();
                x.Property(y => y.Name).IsRequired(true);
                x.Property(y => y.Version);
                x.HasMany(y => y.Members);
                x.Ignore(y => y.PendingEvents);
                x.ToTable(nameof(Project.Model.Project));
            });

            modelBuilder.Entity<User.Model.User>(x =>
            {
                x.HasKey(y => y.Id);
                x.Property(y => y.Id).ValueGeneratedNever();
                x.Property(y => y.FirstName).IsRequired(true);
                x.Property(y => y.LastName).IsRequired(true);
                x.Property(y => y.Email).IsRequired(true);
                x.Property(y => y.Role).IsRequired(true);
                x.HasMany(y => y.Projects);
                x.Property(y => y.AggregateVersion);
                x.ToTable(nameof(User.Model.User));
            });

            modelBuilder.Entity<ProjectUser.ProjectUser>(x =>
            {
                x.HasKey(y => y.Id);
                x.Property(y => y.ProjectId);
                x.Property(y => y.UserId);
                x.ToTable(nameof(ProjectUser.ProjectUser));
            });

            modelBuilder.HasDefaultSchema(SCHEMA);
            base.OnModelCreating(modelBuilder);
        }
    }
}
