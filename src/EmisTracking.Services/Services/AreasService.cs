using AutoMapper;
using EmisTracking.Services.Entities;
using EmisTracking.Services.Interfaces;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EmisTracking.Services.Services
{
    public class AreasService(IRepository<Area> repository, IMapper mapper) : GenericEntityService<Area>(repository, mapper)
    {
        protected override Expression<Func<Area, object>>[] DependenciesIncludes => [];

        protected override Task ValidateAsync(Area item)
        {
            return Task.CompletedTask;
        }
    }

    public class SubdivisionsService(IRepository<Subdivision> repository, IMapper mapper) : GenericEntityService<Subdivision>(repository, mapper)
    {
        protected override Expression<Func<Subdivision, object>>[] DependenciesIncludes =>
        [
            x => x.Area,
        ];

        protected override Task ValidateAsync(Subdivision item)
        {
            return Task.CompletedTask;
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

    public class ConsumptionGroupsService(IRepository<ConsumptionGroup> repository, IMapper mapper) : GenericEntityService<ConsumptionGroup>(repository, mapper)
    {
        protected override Expression<Func<ConsumptionGroup, object>>[] DependenciesIncludes =>
        [
            x => x.Methodology,
        ];

        protected override Task ValidateAsync(ConsumptionGroup item)
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

    public class ConsumptionsService(IRepository<Consumption> repository, IMapper mapper) : GenericEntityService<Consumption>(repository, mapper)
    {
        protected override Expression<Func<Consumption, object>>[] DependenciesIncludes =>
        [
            x => x.ConsumptionGroup
        ];

        protected override Task ValidateAsync(Consumption item)
        {
            return Task.CompletedTask;
        }
    }

    public class ParameterValuesService(IRepository<ParameterValue> repository, IMapper mapper) : GenericEntityService<ParameterValue>(repository, mapper)
    {
        protected override Expression<Func<ParameterValue, object>>[] DependenciesIncludes =>
        [
            x => x.MethodologyParameter,
            x => x.GrossEmission
        ];

        protected override Task ValidateAsync(ParameterValue item)
        {
            return Task.CompletedTask;
        }
    }

    public class GrossEmissionsService(IRepository<GrossEmission> repository, IMapper mapper) : GenericEntityService<GrossEmission>(repository, mapper)
    {
        protected override Expression<Func<GrossEmission, object>>[] DependenciesIncludes =>
        [
            x => x.SourceSubstance,
            x => x.Methodology,
            x => x.Tax
        ];

        protected override Task ValidateAsync(GrossEmission item)
        {
            return Task.CompletedTask;
        }
    }

    public class TaxRatesService(IRepository<TaxRate> repository, IMapper mapper) : GenericEntityService<TaxRate>(repository, mapper)
    {
        protected override Expression<Func<TaxRate, object>>[] DependenciesIncludes => [];

        protected override Task ValidateAsync(TaxRate item)
        {
            return Task.CompletedTask;
        }
    }

    public class TaxesService(IRepository<Tax> repository, IMapper mapper) : GenericEntityService<Tax>(repository, mapper)
    {
        protected override Expression<Func<Tax, object>>[] DependenciesIncludes => [];

        protected override Task ValidateAsync(Tax item)
        {
            return Task.CompletedTask;
        }
    }
}