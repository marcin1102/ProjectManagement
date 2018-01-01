using ProjectManagement.Infrastructure.Storage.EF;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Issue.Model;
using ProjectManagement.Issue.Model.Abstract;
using ProjectManagement.Issue.Repository;

namespace ProjectManagement
{
    public class ProjectManagementContext : BaseDbContext
    {
        private const string SCHEMA = "project-management";

        public ProjectManagementContext(DbContextOptions<ProjectManagementContext> options) : base(options)
        {
        }

        public DbSet<Project.Model.Project> Projects { get; set; }
        public DbSet<User.Model.Member> Users { get; set; }
        public DbSet<Sprint.Model.Sprint> Sprints { get; set; }
        public DbSet<Label.Label> Labels { get; set; }
        public DbSet<AggregateIssue> AggregateIssue { get; set; }
        public DbSet<Issue.Model.Abstract.Issue> Issues { get; set; }        
        public DbSet<Task> Tasks { get; set; }
        public DbSet<ChildBug> ChildBugs { get; set; }
        public DbSet<Bug> Bugs { get; set; }
        
        public DbSet<Nfr> Nfrs { get; set; }
        public DbSet<Subtask> Subtasks { get; set; }
        public DbSet<Comment.Comment> Comments { get; set; }

        public DbSet<IssueLabel> IssuesLabels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project.Model.Project>(x =>
            {
                x.HasKey(y => y.Id);
                x.Property(y => y.Id).ValueGeneratedNever();
                x.Property(y => y.Name).IsRequired();
                x.Property(y => y.members);
                x.Property(y => y.Version);
                x.HasMany(y => y.Labels);
                x.Ignore(y => y.Members);
                x.Ignore(y => y.PendingEvents);
                x.ToTable(nameof(Project.Model.Project));
            });

            modelBuilder.Entity<User.Model.Member>(x =>
            {
                x.HasKey(y => y.Id);
                x.Property(y => y.Id).ValueGeneratedNever();
                x.Property(y => y.FirstName).IsRequired();
                x.Property(y => y.LastName).IsRequired();
                x.Property(y => y.Email).IsRequired();
                x.Property(y => y.Role).IsRequired();
                x.ToTable(nameof(User.Model.Member));
            });

            modelBuilder.Entity<AggregateIssue>(x =>
            {
                x.HasKey(y => y.Id);
                x.Property(y => y.Id).ValueGeneratedNever();
                x.Property(y => y.Version);
                x.Property(y => y.ProjectId).IsRequired();
                x.Property(y => y.ReporterId).ValueGeneratedNever();
                x.Property(y => y.AssigneeId).ValueGeneratedNever();
                x.Property(y => y.Status).IsRequired();
                x.Property(y => y.Title).IsRequired();
                x.Property(y => y.Description).IsRequired();
                x.Property(y => y.SprintId).IsRequired(false);
                x.HasMany(y => y.Comments);
                x.Ignore(y => y.PendingEvents);
                x.Ignore(y => y.Labels);
            });

            modelBuilder.Entity<Issue.Model.Abstract.Issue>(x =>
            {
                x.HasKey(y => y.Id);
                x.Property(y => y.Id).ValueGeneratedNever();
                x.Property(y => y.ProjectId).IsRequired();
                x.Property(y => y.ReporterId).ValueGeneratedNever();
                x.Property(y => y.AssigneeId).ValueGeneratedNever();
                x.Property(y => y.Status).IsRequired();
                x.Property(y => y.Title).IsRequired();
                x.Property(y => y.Description).IsRequired();
                x.Property(y => y.SprintId).IsRequired(false);
                x.HasMany(y => y.Comments);
                x.Ignore(y => y.Labels);
            });

            modelBuilder.Entity<Task>(x =>
            {
                x.HasMany(y => y.Bugs);
                x.HasMany(y => y.Subtasks);
                x.ToTable(nameof(Task));
            });

            modelBuilder.Entity<Subtask>(x =>
            {
                x.ToTable(nameof(Subtask));
            });

            modelBuilder.Entity<ChildBug>(x =>
            {
                x.ToTable(nameof(ChildBug));
            });

            modelBuilder.Entity<Bug>(x =>
            {
                x.ToTable(nameof(ChildBug));
            });

            modelBuilder.Entity<Nfr>(x =>
            {
                x.HasMany(y => y.Bugs);
                x.ToTable(nameof(Nfr));
            });

            modelBuilder.Entity<Sprint.Model.Sprint>(x =>
            {
                x.HasKey(y => y.Id);
                x.Property(y => y.Id).ValueGeneratedNever();
                x.Property(y => y.Name).IsRequired();
                x.Property(y => y.Status).IsRequired();
                x.Property(y => y.StartDate).IsRequired();
                x.Property(y => y.EndDate).IsRequired();
                x.Property(y => y.Version);
                x.Ignore(y => y.PendingEvents);
                x.ToTable(nameof(Sprint.Model.Sprint));
            });

            modelBuilder.Entity<Label.Label>(x =>
            {
                x.HasKey(y => y.Id);
                x.Property(y => y.Id).ValueGeneratedNever();
                x.Property(y => y.ProjectId).ValueGeneratedNever();
                x.Property(y => y.Name).IsRequired();
                x.HasAlternateKey(y => y.Name);
                x.ToTable(nameof(Label.Label));
            });

            modelBuilder.Entity<IssueLabel>(x =>
            {
                x.HasKey(y => new { y.IssueId, y.LabelId });
            });

            modelBuilder.Entity<Comment.Comment>(x =>
            {
                x.HasKey(y => y.Id);
            });

            modelBuilder.HasDefaultSchema(SCHEMA);
            base.OnModelCreating(modelBuilder);
        }
    }
}
