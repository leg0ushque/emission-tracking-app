using AutoMapper;
using EmisTracking.Localization;
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

        public virtual Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            try
            {
                return _repository.GetAll(predicate).ToListAsync();
            }
            catch (InvalidOperationException ex)
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

        public async Task<TEntity> GetByIdAsync(string id)
        {
            try
            {
                return await _repository.GetByIdAsync(id);
            }
            catch (ArgumentNullException ex)
            {
                throw new BusinessLogicException(ex.Message, ex);
            }
        }

        public async virtual Task UpdateAsync(TEntity entity)
        {
            try
            {
                await ValidateAsync(entity);

                var mappedEntity = _mapper.Map<TEntity>(entity);

                await _repository.UpdateAsync(mappedEntity);
            }
            catch (ArgumentException)
            {
                throw new BusinessLogicException(
                    string.Format(LangResources.ItemNotFoundMessageTemplate, nameof(TEntity), entity.Id));
            }
        }

        public Task DeleteAsync(string id)
        {
            try
            {
                return _repository.DeleteAsync(id);
            }
            catch (ArgumentException)
            {
                throw new BusinessLogicException(
                    string.Format(LangResources.ItemNotFoundMessageTemplate, nameof(TEntity), id));
            }
        }

        protected abstract Task ValidateAsync(TEntity item);
    }
}
