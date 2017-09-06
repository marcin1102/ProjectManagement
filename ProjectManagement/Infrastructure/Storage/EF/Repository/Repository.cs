using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Storage.EF.Repository
{
    public class Repository<TEntity>
        where TEntity : class, IEntity
    {
        protected readonly DbContext dbContext;

        private readonly DbSet<TEntity> dbSet;
        public DbSet<TEntity> Query => dbSet;

        public Repository(DbContext dbContext)
        {
            this.dbContext = dbContext;
            dbSet = dbContext.Set<TEntity>();
        }

        public virtual async Task AddAsync(TEntity entity)
        {
            await Query.AddAsync(entity);
            await dbContext.SaveChangesAsync();
        }

        public virtual Task Update(TEntity entity)
        {
            Query.Update(entity);
            return dbContext.SaveChangesAsync();
        }

        public virtual Task<TEntity> GetAsync(Guid id)
        {
            return Query.SingleAsync(x => x.Id == id);
        }

        public virtual Task<TEntity> FindAsync(Guid id)
        {
            return Query.SingleOrDefaultAsync(x => x.Id == id);
        }
    }
}
