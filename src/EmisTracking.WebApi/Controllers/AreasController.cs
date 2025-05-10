using AutoMapper;
using EmisTracking.Services.Entities;
using EmisTracking.Services.Interfaces;
using EmisTracking.WebApi.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EmisTracking.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AreasController : EntityController<AreaViewModel, Area>
    {
        public AreasController(IEntityService<Area> service, IMapper mapper)
        {
            _entityService = service;
            _mapper = mapper;
        }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class SubdivisionsController : EntityController<SubdivisionViewModel, Subdivision>
    {
        public SubdivisionsController(IEntityService<Subdivision> service, IMapper mapper)
        {
            _entityService = service;
            _mapper = mapper;
        }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class ModesController : EntityController<ModeViewModel, Mode>
    {
        public ModesController(IEntityService<Mode> service, IMapper mapper)
        {
            _entityService = service;
            _mapper = mapper;
        }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class MethodologiesController : EntityController<MethodologyViewModel, Methodology>
    {
        public MethodologiesController(IEntityService<Methodology> service, IMapper mapper)
        {
            _entityService = service;
            _mapper = mapper;
        }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class EmissionSourcesController : EntityController<EmissionSourceViewModel, EmissionSource>
    {
        public EmissionSourcesController(IEntityService<EmissionSource> service, IMapper mapper)
        {
            _entityService = service;
            _mapper = mapper;
        }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class OperatingTimesController : EntityController<OperatingTimeViewModel, OperatingTime>
    {
        public OperatingTimesController(IEntityService<OperatingTime> service, IMapper mapper)
        {
            _entityService = service;
            _mapper = mapper;
        }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class PollutantsController : EntityController<PollutantViewModel, Pollutant>
    {
        public PollutantsController(IEntityService<Pollutant> service, IMapper mapper)
        {
            _entityService = service;
            _mapper = mapper;
        }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class SourceSubstancesController : EntityController<SourceSubstanceViewModel, SourceSubstance>
    {
        public SourceSubstancesController(IEntityService<SourceSubstance> service, IMapper mapper)
        {
            _entityService = service;
            _mapper = mapper;
        }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class MethodologyParametersController : EntityController<MethodologyParameterViewModel, MethodologyParameter>
    {
        public MethodologyParametersController(IEntityService<MethodologyParameter> service, IMapper mapper)
        {
            _entityService = service;
            _mapper = mapper;
        }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class ConsumptionGroupsController : EntityController<ConsumptionGroupViewModel, ConsumptionGroup>
    {
        public ConsumptionGroupsController(IEntityService<ConsumptionGroup> service, IMapper mapper)
        {
            _entityService = service;
            _mapper = mapper;
        }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class SpecificIndicatorsController : EntityController<SpecificIndicatorViewModel, SpecificIndicator>
    {
        public SpecificIndicatorsController(IEntityService<SpecificIndicator> service, IMapper mapper)
        {
            _entityService = service;
            _mapper = mapper;
        }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class ConsumptionsController : EntityController<ConsumptionViewModel, Consumption>
    {
        public ConsumptionsController(IEntityService<Consumption> service, IMapper mapper)
        {
            _entityService = service;
            _mapper = mapper;
        }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class ParameterValuesController : EntityController<ParameterValueViewModel, ParameterValue>
    {
        public ParameterValuesController(IEntityService<ParameterValue> service, IMapper mapper)
        {
            _entityService = service;
            _mapper = mapper;
        }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class GrossEmissionsController : EntityController<GrossEmissionViewModel, GrossEmission>
    {
        public GrossEmissionsController(IEntityService<GrossEmission> service, IMapper mapper)
        {
            _entityService = service;
            _mapper = mapper;
        }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class TaxRatesController : EntityController<TaxRateViewModel, TaxRate>
    {
        public TaxRatesController(IEntityService<TaxRate> service, IMapper mapper)
        {
            _entityService = service;
            _mapper = mapper;
        }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class TaxesController : EntityController<TaxViewModel, Tax>
    {
        public TaxesController(IEntityService<Tax> service, IMapper mapper)
        {
            _entityService = service;
            _mapper = mapper;
        }
    }
}
