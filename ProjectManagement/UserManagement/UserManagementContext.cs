using Infrastructure.Storage.EF;
using Microsoft.EntityFrameworkCore;
using UserManagement.Authentication;

namespace UserManagement
{
    public class UserManagementContext : BaseDbContext
    {
        private const string SCHEMA = "user-management";

        public DbSet<User.Model.User> Users { get; set; }
        public DbSet<Token> Tokens { get; set; }

        public UserManagementContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User.Model.User>(x =>
            {
                x.HasKey(y => y.Id);
                x.Property(y => y.Id).ValueGeneratedNever();
                x.HasAlternateKey(y => y.Email);
                x.Property(y => y.FirstName).IsRequired();
                x.Property(y => y.LastName).IsRequired();
                x.Property(y => y.Role).IsRequired();
                x.Property(y => y.Password).IsRequired();
                x.Property(y => y.Version);
                x.Ignore(y => y.PendingEvents);
                x.ToTable(nameof(User.Model.User));
            });

            modelBuilder.Entity<Token>(x =>
            {
                x.HasKey(y => y.Value);
                x.Property(y => y.Value).ValueGeneratedNever();
                x.Property(y => y.UserId);
                x.Property(y => y.UserId).ValueGeneratedNever();
                x.Property(y => y.LastlyUsed);
                x.Property(y => y.LastlyUsed).ValueGeneratedNever();
            });

            modelBuilder.HasDefaultSchema(SCHEMA);
            base.OnModelCreating(modelBuilder);
        }
    }
}
