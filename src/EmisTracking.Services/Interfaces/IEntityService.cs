using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EmisTracking.Services.Interfaces
{
    public interface IEntityService<TEntity>
    {
        Task<TEntity> GetByIdAsync(string id);

        Task<string> AddAsync(TEntity entity);

        Task UpdateAsync(TEntity entity);

        Task DeleteAsync(string id);

        Task<List<TEntity>> GetAllAtPageAsync(int? pageNumber, int? pageSize);

        Task<int> GetTotalPagesCountAsync(int? pageSize);
    }
}
