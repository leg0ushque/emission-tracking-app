using System;
using System.Linq;
using System.Threading.Tasks;

namespace EmisTracking.Services.Interfaces
{
    public interface IRepository<TEntity>
        where TEntity : class, IEntity
    {
        Task<string> CreateAsync(TEntity entity);

        IQueryable<TEntity> GetAll(Func<TEntity, bool>? predicate = null);

        Task<TEntity> GetByIdAsync(string entityId);

        Task UpdateAsync(TEntity entity);

        Task DeleteAsync(string entityId);
    }
}
