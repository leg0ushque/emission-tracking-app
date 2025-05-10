using EmisTracking.Services.Database.Contexts;
using EmisTracking.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;


namespace EmisTracking.Services.Database.Repositories
{
    public class GenericRepository<TEntity>(EmissionDbContext context) : IRepository<TEntity>
        where TEntity : class, IEntity
    {
        private readonly EmissionDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

        public async Task<string> CreateAsync(TEntity entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            _context.Entry(entity).State = EntityState.Detached;
            return entity.Id;
        }

        public async Task<TEntity> GetByIdAsync(string entityId)
        {
            ArgumentNullException.ThrowIfNull(entityId);

            var entity = await _context.Set<TEntity>()
                .FirstOrDefaultAsync(e => e.Id == entityId);

            if (entity != null)
            {
                _context.Entry(entity).State = EntityState.Detached;
            }

            return entity;
        }

        public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null)
        {
            return predicate == null ? _context.Set<TEntity>().AsNoTracking()
                : _context.Set<TEntity>().Where(predicate).AsQueryable().AsNoTracking();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            var entityFound = await _context.Set<TEntity>()
                .AnyAsync(e => e.Id == entity.Id);

            if (!entityFound)
            {
                throw new ArgumentException(null, nameof(entity));
            }

            _context.Update(entity);
            await _context.SaveChangesAsync();
            _context.Entry(entity).State = EntityState.Detached;
        }

        public async Task DeleteAsync(string entityId)
        {
            var entity = await GetByIdAsync(entityId)
                ?? throw new ArgumentException(null, nameof(entityId));

            _context.Remove(entity);
            await _context.SaveChangesAsync();
            _context.Entry(entity).State = EntityState.Detached;
        }
    }
}
