using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Message;
using Infrastructure.Storage.EF;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Storage.EF.EventContext
{
    public class EventContext : BaseDbContext
    {
        private const string SCHEMA = "Events";

        public DbSet<EventEnvelope> Events { get; set; }

        public EventContext(DbContextOptions options) : base(options)
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

            modelBuilder.HasDefaultSchema(SCHEMA);
            base.OnModelCreating(modelBuilder);
        }
    }
}
