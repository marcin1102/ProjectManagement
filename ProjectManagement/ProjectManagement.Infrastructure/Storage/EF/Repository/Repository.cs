using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Infrastructure.Primitives.Exceptions;

namespace ProjectManagement.Infrastructure.Storage.EF.Repository
{
    public interface IRepository<TEntity>
         where TEntity : class, IEntity
    {
        Task AddAsync(TEntity entity);
        Task Update(TEntity entity);
        Task<TEntity> GetAsync(Guid id);
        Task<TEntity> FindAsync(Guid id);
    }

    public class Repository<TEntity> : IRepository<TEntity>
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

        public virtual async Task<TEntity> GetAsync(Guid id)
        {
            var entity = await Query.SingleOrDefaultAsync(x => x.Id == id);
            if (entity == null)
                throw new EntityDoesNotExist(id, typeof(TEntity).Name);
            return entity;
        }

        public virtual Task<TEntity> FindAsync(Guid id)
        {
            return Query.SingleOrDefaultAsync(x => x.Id == id);
        }
    }
}
