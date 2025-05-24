using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EmisTracking.Services.Interfaces
{
    public interface IEntityService<TEntity>
    {
        Task<TEntity> GetByIdAsync(string id, bool loadDependencies = false);

        Task<string> AddAsync(TEntity entity);

        Task<bool> UpdateAsync(TEntity entity);

        Task<bool> DeleteAsync(string id);

        Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null, bool loadDependencies = false);
    }
}
