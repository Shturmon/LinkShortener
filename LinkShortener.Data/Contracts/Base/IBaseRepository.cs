using System;
using System.Linq;
using System.Threading.Tasks;

namespace LinkShortener.Data.Contracts.Base
{
    public interface IBaseRepository { }

    public interface IBaseRepository<TEntity> : IBaseRepository
    {
        IQueryable<TEntity> All { get; }
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        Task<TEntity> GetByIdAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);
    }
}
