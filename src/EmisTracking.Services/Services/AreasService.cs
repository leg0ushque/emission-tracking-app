using AutoMapper;
using EmisTracking.Services.Entities;
using EmisTracking.Services.Interfaces;
using System.Threading.Tasks;

namespace EmisTracking.Services.Services
{
    public class AreasService : GenericEntityService<Area>
    {
        public AreasService(IRepository<Area> repository, IMapper mapper)
            : base(repository, mapper)
        { }

        protected override Task ValidateAsync(Area item)
        {
            return Task.CompletedTask;
        }
    }

    public class SubdivisionsService : GenericEntityService<Subdivision>
    {
        public SubdivisionsService(IRepository<Subdivision> repository, IMapper mapper)
            : base(repository, mapper)
        { }

        protected override Task ValidateAsync(Subdivision item)
        {
            return Task.CompletedTask;
        }
    }

    public class ModesService : GenericEntityService<Mode>
    {
        public ModesService(IRepository<Mode> repository, IMapper mapper)
            : base(repository, mapper)
        { }

        protected override Task ValidateAsync(Mode item)
        {
            return Task.CompletedTask;
        }
    }

    public class MethodologiesService : GenericEntityService<Methodology>
    {
        public MethodologiesService(IRepository<Methodology> repository, IMapper mapper)
            : base(repository, mapper)
        { }

        protected override Task ValidateAsync(Methodology item)
        {
            return Task.CompletedTask;
        }
    }

    public class EmissionSourcesService : GenericEntityService<EmissionSource>
    {
        public EmissionSourcesService(IRepository<EmissionSource> repository, IMapper mapper)
            : base(repository, mapper)
        { }

        protected override Task ValidateAsync(EmissionSource item)
        {
            return Task.CompletedTask;
        }
    }

    public class OperatingTimesService : GenericEntityService<OperatingTime>
    {
        public OperatingTimesService(IRepository<OperatingTime> repository, IMapper mapper)
            : base(repository, mapper)
        { }

        protected override Task ValidateAsync(OperatingTime item)
        {
            return Task.CompletedTask;
        }
    }

    public class PollutantsService : GenericEntityService<Pollutant>
    {
        public PollutantsService(IRepository<Pollutant> repository, IMapper mapper)
            : base(repository, mapper)
        { }

        protected override Task ValidateAsync(Pollutant item)
        {
            return Task.CompletedTask;
        }
    }

    public class SourceSubstancesService : GenericEntityService<SourceSubstance>
    {
        public SourceSubstancesService(IRepository<SourceSubstance> repository, IMapper mapper)
            : base(repository, mapper)
        { }

        protected override Task ValidateAsync(SourceSubstance item)
        {
            return Task.CompletedTask;
        }
    }

    public class MethodologyParametersService : GenericEntityService<MethodologyParameter>
    {
        public MethodologyParametersService(IRepository<MethodologyParameter> repository, IMapper mapper)
            : base(repository, mapper)
        { }

        protected override Task ValidateAsync(MethodologyParameter item)
        {
            return Task.CompletedTask;
        }
    }

    public class ConsumptionGroupsService : GenericEntityService<ConsumptionGroup>
    {
        public ConsumptionGroupsService(IRepository<ConsumptionGroup> repository, IMapper mapper)
            : base(repository, mapper)
        { }

        protected override Task ValidateAsync(ConsumptionGroup item)
        {
            return Task.CompletedTask;
        }
    }

    public class SpecificIndicatorsService : GenericEntityService<SpecificIndicator>
    {
        public SpecificIndicatorsService(IRepository<SpecificIndicator> repository, IMapper mapper)
            : base(repository, mapper)
        { }

        protected override Task ValidateAsync(SpecificIndicator item)
        {
            return Task.CompletedTask;
        }
    }

    public class ConsumptionsService : GenericEntityService<Consumption>
    {
        public ConsumptionsService(IRepository<Consumption> repository, IMapper mapper)
            : base(repository, mapper)
        { }

        protected override Task ValidateAsync(Consumption item)
        {
            return Task.CompletedTask;
        }
    }

    public class ParameterValuesService : GenericEntityService<ParameterValue>
    {
        public ParameterValuesService(IRepository<ParameterValue> repository, IMapper mapper)
            : base(repository, mapper)
        { }

        protected override Task ValidateAsync(ParameterValue item)
        {
            return Task.CompletedTask;
        }
    }

    public class GrossEmissionsService : GenericEntityService<GrossEmission>
    {
        public GrossEmissionsService(IRepository<GrossEmission> repository, IMapper mapper)
            : base(repository, mapper)
        { }

        protected override Task ValidateAsync(GrossEmission item)
        {
            return Task.CompletedTask;
        }
    }

    public class TaxRatesService : GenericEntityService<TaxRate>
    {
        public TaxRatesService(IRepository<TaxRate> repository, IMapper mapper)
            : base(repository, mapper)
        { }

        protected override Task ValidateAsync(TaxRate item)
        {
            return Task.CompletedTask;
        }
    }

    public class TaxesService : GenericEntityService<Tax>
    {
        public TaxesService(IRepository<Tax> repository, IMapper mapper)
            : base(repository, mapper)
        { }

        protected override Task ValidateAsync(Tax item)
        {
            return Task.CompletedTask;
        }
    }
}