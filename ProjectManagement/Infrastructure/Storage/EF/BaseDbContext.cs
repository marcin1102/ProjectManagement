using Infrastructure.Message;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Storage.EF
{
    public abstract class BaseDbContext : DbContext
    {
        public DbSet<EventEnvelope> Events { get; set; }

        public BaseDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EventEnvelope>(x =>
            {
                x.HasKey(y => y.Id);
                x.Property(y => y.DomainEvent).IsRequired(true);
                x.Property(y => y.DomainEventType).IsRequired(true);
                x.ToTable(nameof(EventEnvelope));
            });

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
            base.OnConfiguring(optionsBuilder);
        }
    }
}
