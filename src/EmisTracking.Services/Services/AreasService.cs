using AutoMapper;
using EmisTracking.Localization;
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
    public class AreasService(IRepository<Area> repository, IMapper mapper) : GenericEntityService<Area>(repository, mapper)
    {
        protected override Expression<Func<Area, object>>[] DependenciesIncludes => [];

        protected override async Task ValidateAsync(Area item)
        {
            var errors = new List<FieldError>();

            if (string.IsNullOrEmpty(item.Name))
            {
                errors.Add(new FieldError
                {
                    Field = nameof(item.Name),
                    Message = LangResources.MustBeFilledMessage
                });
            }

            var existingArea = await _repository.GetAll(a =>
                a.Id != item.Id && a.Name == item.Name)
                .ToListAsync();

            if (existingArea != null)
            {
                errors.Add(new FieldError
                {
                    Field = string.Empty,
                    Message = "Уже существует площадка с тем же именем"
                });
            }

            if (errors.Count != 0)
            {
                throw new BusinessLogicException { FieldErrors = errors };
            }
        }
    }

    public class ModesService(IRepository<Mode> repository, IMapper mapper) : GenericEntityService<Mode>(repository, mapper)
    {
        protected override Expression<Func<Mode, object>>[] DependenciesIncludes => [];

        protected override Task ValidateAsync(Mode item)
        {
            return Task.CompletedTask;
        }
    }

    public class MethodologiesService(IRepository<Methodology> repository, IMapper mapper) : GenericEntityService<Methodology>(repository, mapper)
    {
        protected override Expression<Func<Methodology, object>>[] DependenciesIncludes =>
        [
            x => x.Mode,
        ];

        protected override Task ValidateAsync(Methodology item)
        {
            return Task.CompletedTask;
        }
    }

    public class EmissionSourcesService(IRepository<EmissionSource> repository, IMapper mapper) : GenericEntityService<EmissionSource>(repository, mapper)
    {
        protected override Expression<Func<EmissionSource, object>>[] DependenciesIncludes =>
        [
            x => x.Mode,
            x => x.Subdivision,
            x => x.Methodology,
        ];

        protected override Task ValidateAsync(EmissionSource item)
        {
            // do not forget - either mode or methodology!
            return Task.CompletedTask;
        }
    }

    public class OperatingTimesService(IRepository<OperatingTime> repository, IMapper mapper) : GenericEntityService<OperatingTime>(repository, mapper)
    {
        protected override Expression<Func<OperatingTime, object>>[] DependenciesIncludes =>
        [
            x => x.EmissionSource,
        ];

        public override Task<List<OperatingTime>> GetAllAsync(
            Expression<Func<OperatingTime, bool>> predicate = null, bool loadDependencies = false)
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

        protected override Task ValidateAsync(OperatingTime item)
        {
            return Task.CompletedTask;
        }
    }

    public class PollutantsService(IRepository<Pollutant> repository, IMapper mapper) : GenericEntityService<Pollutant>(repository, mapper)
    {
        protected override Expression<Func<Pollutant, object>>[] DependenciesIncludes => [];

        protected override Task ValidateAsync(Pollutant item)
        {
            return Task.CompletedTask;
        }
    }

    public class SourceSubstancesService(IRepository<SourceSubstance> repository, IMapper mapper) : GenericEntityService<SourceSubstance>(repository, mapper)
    {
        protected override Expression<Func<SourceSubstance, object>>[] DependenciesIncludes =>
        [
            x => x.EmissionSource,
            x => x.Pollutant,
        ];

        protected override Task ValidateAsync(SourceSubstance item)
        {
            return Task.CompletedTask;
        }
    }

    public class MethodologyParametersService(IRepository<MethodologyParameter> repository, IMapper mapper) : GenericEntityService<MethodologyParameter>(repository, mapper)
    {
        protected override Expression<Func<MethodologyParameter, object>>[] DependenciesIncludes =>
        [
            x => x.Methodology,
        ];

        protected override Task ValidateAsync(MethodologyParameter item)
        {
            return Task.CompletedTask;
        }
    }

    public class SpecificIndicatorsService(IRepository<SpecificIndicator> repository, IMapper mapper) : GenericEntityService<SpecificIndicator>(repository, mapper)
    {
        protected override Expression<Func<SpecificIndicator, object>>[] DependenciesIncludes =>
        [
            x => x.ConsumptionGroup,
            x => x.Pollutant,
        ];

        protected override Task ValidateAsync(SpecificIndicator item)
        {
            return Task.CompletedTask;
        }
    }

    public class ParameterValuesService(IRepository<ParameterValue> repository, IMapper mapper) : GenericEntityService<ParameterValue>(repository, mapper)
    {
        protected override Expression<Func<ParameterValue, object>>[] DependenciesIncludes =>
        [
            x => x.MethodologyParameter,
            x => x.SourceSubstance,
        ];

        protected override Task ValidateAsync(ParameterValue item)
        {
            return Task.CompletedTask;
        }

        public override Task<List<ParameterValue>> GetAllAsync(
            Expression<Func<ParameterValue, bool>> predicate = null, bool loadDependencies = false)
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

    public class TaxRatesService(IRepository<TaxRate> repository, IMapper mapper) : GenericEntityService<TaxRate>(repository, mapper)
    {
        protected override Expression<Func<TaxRate, object>>[] DependenciesIncludes => [];

        protected override Task ValidateAsync(TaxRate item)
        {
            return Task.CompletedTask;
        }

        public override Task<List<TaxRate>> GetAllAsync(
            Expression<Func<TaxRate, bool>> predicate = null, bool loadDependencies = false)
        {
            try
            {
                return (loadDependencies ?
                    _repository.GetAll(predicate, DependenciesIncludes)
                    : _repository.GetAll(predicate))
                        .OrderByDescending(x => x.StartDate)
                        .ToListAsync();
            }
            catch (InvalidOperationException ex)
            {
                throw new BusinessLogicException(ex.Message, ex);
            }
        }
    }

    public class TaxesService(IRepository<Tax> repository, IMapper mapper) : GenericEntityService<Tax>(repository, mapper)
    {
        protected override Expression<Func<Tax, object>>[] DependenciesIncludes => [];

        protected override Task ValidateAsync(Tax item)
        {
            return Task.CompletedTask;
        }

        public override Task<List<Tax>> GetAllAsync(
            Expression<Func<Tax, bool>> predicate = null, bool loadDependencies = false)
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