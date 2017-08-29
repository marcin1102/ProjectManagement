using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Storage.EF
{
    public class BaseDbContext : DbContext
    {
        protected string schema;
        public BaseDbContext(DbContextOptions options, string schema) : base(options)
        {
            this.schema = schema;
        }
    }
}
