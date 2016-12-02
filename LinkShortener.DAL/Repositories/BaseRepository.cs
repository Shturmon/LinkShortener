using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using LinkShortener.Data.Contracts.Base;
using LinkShortener.Data.Entities.Base;
using LinkShortener.DAL.Contracts;

namespace LinkShortener.DAL.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity>
                    where TEntity : BaseEntity
    {
        protected ILinkShortenerDbContext Context { get; }
        protected DbSet<TEntity> DbSet { get; set; }

        public IQueryable<TEntity> All => DbSet;

        public BaseRepository(ILinkShortenerDbContext dbContext)
        {
            if (dbContext == null)
                throw new ArgumentNullException(nameof(dbContext));

            Context = dbContext;
            DbSet = Context.DbContext.Set<TEntity>();
        }

        public async Task<TEntity> GetByIdAsync(Guid id)
        {
            return await DbSet.FindAsync(id);
        }

        public virtual void Add(TEntity entity)
        {
            var baseEntity = entity as BaseEntity;
            if (baseEntity != null && baseEntity.Id == Guid.Empty)
            {
                baseEntity.Id = Guid.NewGuid();
            }

            DbEntityEntry dbEntityEntry = Context.DbContext.Entry(entity);
            if (dbEntityEntry.State != EntityState.Detached)
            {
                dbEntityEntry.State = EntityState.Added;
            }
            else
            {
                DbSet.Add(entity);
            }
        }

        public virtual void Update(TEntity entity)
        {
            DbEntityEntry dbEntityEntry = Context.DbContext.Entry(entity);
            if (dbEntityEntry.State == EntityState.Detached)
            {
                DbSet.Attach(entity);
            }
            dbEntityEntry.State = EntityState.Modified;
        }

        public virtual void Delete(TEntity entity)
        {
            DbEntityEntry dbEntityEntry = Context.DbContext.Entry(entity);
            if (dbEntityEntry.State != EntityState.Deleted)
            {
                dbEntityEntry.State = EntityState.Deleted;
            }
            else
            {
                DbSet.Attach(entity);
                DbSet.Remove(entity);
            }
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await DbSet.FindAsync(id) != null;
        }
    }
}
