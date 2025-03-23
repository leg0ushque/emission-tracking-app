using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmisTracking.Services.Interfaces
{
    public interface IPaginatedRepository<TEntity> : IRepository<TEntity>
        where TEntity : class, IEntity
    {
        public Task<List<TEntity>> GetAllAtPageAsync(IQueryable<TEntity> itemsQuery,
            int? pageNumber = Constants.MinPageNumber,
            int? pageSize = Constants.DefaultPageSize);

        public Task<int> GetTotalPagesCountAsync(IQueryable<TEntity> itemsQuery, int? pageSize = Constants.DefaultPageSize);
    }
}
