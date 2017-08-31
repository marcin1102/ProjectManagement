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

        public virtual Task Add(TEntity entity)
        {
            return Query.AddAsync(entity);
        }

        public virtual void Update(TEntity entity)
        {
            Query.Update(entity);
        }

        public virtual Task<TEntity> Get(Guid id)
        {
            return Query.SingleAsync(x => x.Id == id);
        }

        public virtual Task<TEntity> Find(Guid id)
        {
            return Query.SingleOrDefaultAsync(x => x.Id == id);
        }
    }
}
