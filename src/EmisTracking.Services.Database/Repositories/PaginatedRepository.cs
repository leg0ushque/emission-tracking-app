using EmisTracking.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;
using EmisTracking.Services.Database.Contexts;
using ServicesConstants = EmisTracking.Services.Constants;


namespace EmisTracking.Services.Database.Repositories
{
    public class PaginatedRepository<TEntity>(EmissionDbContext context) :
        GenericRepository<TEntity>(context), IPaginatedRepository<TEntity>
        where TEntity : class, IEntity
    {
        private const int ExtraPage = 1;
        private const int NoExtraPage = 0;

        public async Task<List<TEntity>> GetAllAtPageAsync(IQueryable<TEntity> itemsQuery, int? pageNumber = ServicesConstants.MinPageNumber, int? pageSize = ServicesConstants.DefaultPageSize)
        {
            if (pageSize.Value < ServicesConstants.MinPageSize)
            {
                throw new InvalidOperationException($"Page size value should be greater or equal {ServicesConstants.MinPageSize}.");
            }

            var itemsCount = await itemsQuery.CountAsync();
            var skipCount = (pageNumber.Value - 1) * pageSize.Value;

            if (skipCount >= itemsCount)
            {
                return Enumerable.Empty<TEntity>().ToList();
            }

            var lastPageNumber = GetPagesCount(itemsCount, pageSize);

            if (pageNumber < ServicesConstants.MinPageNumber || pageNumber.Value > lastPageNumber)
            {
                throw new InvalidOperationException($"Page number value should be between {ServicesConstants.MinPageNumber} and {lastPageNumber} for size of {pageSize.Value} items per page.");
            }

            return await itemsQuery
                .Skip(skipCount)
                .Take(pageSize.Value)
                .ToListAsync();
        }

        public async Task<int> GetTotalPagesCountAsync(IQueryable<TEntity> itemsQuery, int? pageSize = ServicesConstants.DefaultPageSize)
        {
            if (pageSize.Value < ServicesConstants.MinPageSize)
            {
                throw new InvalidOperationException($"Page size must be at least {ServicesConstants.MinPageSize}.");
            }

            var itemsCount = await itemsQuery.CountAsync();

            return GetPagesCount(itemsCount, pageSize);
        }

        private static int GetPagesCount(int itemsCount, int? pageSize)
            => itemsCount / pageSize.Value + CheckExtraPage(itemsCount, pageSize);

        // for 7 items per page:
        // 35 items = 5 pages, 37 items = 6 pages
        private static int CheckExtraPage(int itemsCount, int? pageSize)
            => itemsCount % pageSize.Value != 0 ? ExtraPage : NoExtraPage;
    }
}
