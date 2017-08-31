using Infrastructure.Message;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Storage.EF
{
    public class BaseDbContext : DbContext
    {

        public BaseDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
