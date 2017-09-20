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
        public DbSet<Label.Label> Labels { get; set; }
        public DbSet<IssueSubtasks.IssueSubtask> IssuesSubtasks { get; set; }
        public DbSet<IssueLabel.IssueLabel> IssuesLabels { get; set; }
        public DbSet<Issue.Model.Issue> Issues { get; set; }

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

            modelBuilder.Entity<Issue.Model.Issue>(x =>
            {
                x.HasKey(y => y.Id);
                x.Property(y => y.Id).ValueGeneratedNever();
                x.Property(y => y.Version);
                x.Property(y => y.ProjectId).IsRequired();
                x.HasOne(y => y.Reporter).WithMany();
                x.HasOne(y => y.Assignee).WithMany();
                x.Property(y => y.Status).IsRequired();
                x.Property(y => y.Title).IsRequired();
                x.Property(y => y.Type).IsRequired();
                x.Property(y => y.Description).IsRequired();
                x.HasMany(y => y.Labels).WithOne();
                x.HasMany(y => y.Subtasks).WithOne();
                x.Property(y => y.comments);
                x.Ignore(y => y.Comments);
                x.Ignore(y => y.PendingEvents);
                x.ToTable(nameof(Issue.Model.Issue));
            });

            modelBuilder.Entity<IssueLabel.IssueLabel>(x =>
            {
                x.HasKey(y => y.Id);
                x.ToTable(nameof(IssueLabel.IssueLabel));
            });

            modelBuilder.Entity<IssueSubtasks.IssueSubtask>(x =>
            {
                x.HasKey(y => y.Id);
                x.Property(y => y.ProjectId).IsRequired();
                x.Property(y => y.IssueId).IsRequired();
                x.Property(y => y.SubtaskId).IsRequired();
                x.ToTable(nameof(IssueSubtasks.IssueSubtask));
            });

            modelBuilder.HasDefaultSchema(SCHEMA);
            base.OnModelCreating(modelBuilder);
        }
    }
}
