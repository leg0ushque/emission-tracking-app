using AutoMapper;
using EmisTracking.Services.Exceptions;
using EmisTracking.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EmisTracking.Services.Services
{
    public abstract class GenericEntityService<TEntity>(IRepository<TEntity> repository, IMapper mapper)
        : IEntityService<TEntity>
        where TEntity : class, IEntity
    {
        private protected readonly IRepository<TEntity> _repository = repository;
        private protected readonly IMapper _mapper = mapper;

        protected abstract Expression<Func<TEntity, object>>[] DependenciesIncludes { get; }

        public virtual Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null, bool loadDependencies = false)
        {
            try
            {
                return loadDependencies ?
                    _repository.GetAll(predicate, DependenciesIncludes).ToListAsync()
                    : _repository.GetAll(predicate).ToListAsync();
            }
            catch (InvalidOperationException ex)
            {
                throw new BusinessLogicException(ex.Message, ex);
            }
        }

        public Task<TEntity> GetByIdAsync(string id, bool loadDependencies = false)
        {
            try
            {
                return loadDependencies ?
                    _repository.GetByIdAsync(id, DependenciesIncludes)
                    : _repository.GetByIdAsync(id);
            }
            catch (ArgumentNullException ex)
            {
                throw new BusinessLogicException(ex.Message, ex);
            }
        }

        public virtual async Task<string> AddAsync(TEntity entity)
        {
            try
            {
                await ValidateAsync(entity);

                return await _repository.CreateAsync(_mapper.Map<TEntity>(entity));
            }
            catch (ArgumentNullException ex)
            {
                throw new BusinessLogicException(ex.Message, ex);
            }
            catch (DbUpdateException ex)
            {
                throw new BusinessLogicException(ex.Message, ex);
            }
        }

        public async virtual Task<bool> UpdateAsync(TEntity entity)
        {
            try
            {
                await ValidateAsync(entity);

                var mappedEntity = _mapper.Map<TEntity>(entity);

                return await _repository.UpdateAsync(mappedEntity);
            }
            catch (ArgumentNullException ex)
            {
                throw new BusinessLogicException(ex.Message, ex);
            }
        }

        public Task<bool> DeleteAsync(string id)
        {
            try
            {
                return _repository.DeleteAsync(id);
            }
            catch (ArgumentNullException ex)
            {
                throw new BusinessLogicException(ex.Message, ex);
            }
        }

        protected abstract Task ValidateAsync(TEntity item);
    }
}