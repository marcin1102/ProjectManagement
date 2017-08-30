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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project.Model.Project>(x =>
            {
                x.HasKey(y => y.Id);
                x.Property(y => y.Name).IsRequired(true);
                x.ToTable(nameof(Project.Model.Project));
            });

            modelBuilder.HasDefaultSchema(SCHEMA);
            base.OnModelCreating(modelBuilder);
        }
    }
}
