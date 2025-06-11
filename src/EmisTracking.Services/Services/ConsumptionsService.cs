using AutoMapper;
using EmisTracking.Services.Entities;
using EmisTracking.Services.Exceptions;
using EmisTracking.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EmisTracking.Services.Services
{
    public class ConsumptionsService(IRepository<Consumption> repository, IMapper mapper)
        : GenericEntityService<Consumption>(repository, mapper)
    {
        protected override Expression<Func<Consumption, object>>[] DependenciesIncludes =>
        [
            x => x.ConsumptionGroup
        ];

        protected override Task ValidateAsync(Consumption item)
        {
            return Task.CompletedTask;
        }

        public override Task<List<Consumption>> GetAllAsync(
            Expression<Func<Consumption, bool>> predicate = null, bool loadDependencies = false)
        {
            try
            {
                return (loadDependencies ?
                    _repository.GetAll(predicate, DependenciesIncludes)
                    : _repository.GetAll(predicate))
                        .OrderByDescending(x => x.Year)
                        .ThenByDescending(x => x.Month)
                        .ToListAsync();
            }
            catch (InvalidOperationException ex)
            {
                throw new BusinessLogicException(ex.Message, ex);
            }
        }
    }
}