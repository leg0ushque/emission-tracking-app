using AutoMapper;
using EmisTracking.Localization;
using EmisTracking.Services.Exceptions;
using EmisTracking.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmisTracking.Services.Services
{
    public abstract class GenericEntityService<TEntity>(IPaginatedRepository<TEntity> repository, IMapper mapper)
        : IEntityService<TEntity>
        where TEntity : class, IEntity
    {
        private protected readonly IPaginatedRepository<TEntity> _repository = repository;
        private protected readonly IMapper _mapper = mapper;

        public async Task<List<TEntity>> GetAllAtPageAsync(int? pageNumber, int? pageSize)
        {
            try
            {
                var allItemsQuery = _repository.GetAll();

                return await _repository.GetAllAtPageAsync(allItemsQuery, pageNumber, pageSize);
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

                await ValidateDuplicateCreation(entity);

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

        public async Task<int> GetTotalPagesCountAsync(int? pageSize)
        {
            try
            {
                var allItemsQuery = _repository.GetAll();

                return await _repository.GetTotalPagesCountAsync(allItemsQuery, pageSize);
            }
            catch (InvalidOperationException ex)
            {
                throw new BusinessLogicException(ex.Message, ex);
            }
        }

        public async Task<TEntity> GetById(string entityId)
        {
            try
            {
                return await _repository.GetByIdAsync(entityId);
            }
            catch (ArgumentException)
            {
                throw new BusinessLogicException(
                    string.Format(LangResources.ItemNotFoundMessageTemplate, nameof(TEntity), entityId));
            }
        }

        public async virtual Task UpdateAsync(TEntity entity)
        {
            try
            {
                await ValidateAsync(entity);

                var mappedEntity = _mapper.Map<TEntity>(entity);

                await _repository.CreateAsync(mappedEntity);
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

        protected abstract Task ValidateDuplicateCreation(TEntity item);
    }
}
