using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EmisTracking.Services.Interfaces
{
    public interface IRepository<TEntity>
        where TEntity : class, IEntity
    {
        Task<string> CreateAsync(TEntity entity);

        IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null,
            params Expression<Func<TEntity, object>>[] includes);

        Task<TEntity> GetByIdAsync(string entityId,
            params Expression<Func<TEntity, object>>[] includes);

        Task UpdateAsync(TEntity entity);

        Task DeleteAsync(string entityId);
    }
}
