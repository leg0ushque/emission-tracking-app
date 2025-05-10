using EmisTracking.Localization.StudentsPerf.Localization;
using EmisTracking.Services.WebApi.Services;
using EmisTracking.WebApi.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EmisTracking.WebApp.Controllers
{
    [Route("[controller]")]
    public class AreasController : BaseViewController<AreaViewModel>
    {
        public AreasController(IBaseApiService<AreaViewModel> apiService)
        {
            _apiService = apiService;
        }

        protected override string CreationTitle => LangResources.Titles.AreasCreate;
        protected override string UpdateTitle => LangResources.Titles.AreasUpdate;
    }

    [Route("[controller]")]
    public class SubdivisionsController : BaseViewController<SubdivisionViewModel>
    {
        public SubdivisionsController(IBaseApiService<SubdivisionViewModel> apiService)
        {
            _apiService = apiService;
        }

        protected override string CreationTitle => LangResources.Titles.SubdivisionsCreate;
        protected override string UpdateTitle => LangResources.Titles.SubdivisionsUpdate;
    }

    [Route("[controller]")]
    public class ModesController : BaseViewController<ModeViewModel>
    {
        public ModesController(IBaseApiService<ModeViewModel> apiService)
        {
            _apiService = apiService;
        }

        protected override string CreationTitle => LangResources.Titles.ModesCreate;
        protected override string UpdateTitle => LangResources.Titles.ModesUpdate;
    }

    [Route("[controller]")]
    public class MethodologiesController : BaseViewController<MethodologyViewModel>
    {
        public MethodologiesController(IBaseApiService<MethodologyViewModel> apiService)
        {
            _apiService = apiService;
        }

        protected override string CreationTitle => LangResources.Titles.MethodologiesCreate;
        protected override string UpdateTitle => LangResources.Titles.MethodologiesUpdate;
    }

    [Route("[controller]")]
    public class EmissionSourcesController : BaseViewController<EmissionSourceViewModel>
    {
        public EmissionSourcesController(IBaseApiService<EmissionSourceViewModel> apiService)
        {
            _apiService = apiService;
        }

        protected override string CreationTitle => LangResources.Titles.EmissionSourcesCreate;
        protected override string UpdateTitle => LangResources.Titles.EmissionSourcesUpdate;
    }

    [Route("[controller]")]
    public class OperatingTimesController : BaseViewController<OperatingTimeViewModel>
    {
        public OperatingTimesController(IBaseApiService<OperatingTimeViewModel> apiService)
        {
            _apiService = apiService;
        }

        protected override string CreationTitle => LangResources.Titles.OperatingTimesCreate;
        protected override string UpdateTitle => LangResources.Titles.OperatingTimesUpdate;
    }

    [Route("[controller]")]
    public class PollutantsController : BaseViewController<PollutantViewModel>
    {
        public PollutantsController(IBaseApiService<PollutantViewModel> apiService)
        {
            _apiService = apiService;
        }

        protected override string CreationTitle => LangResources.Titles.PollutantsCreate;
        protected override string UpdateTitle => LangResources.Titles.PollutantsUpdate;
    }

    [Route("[controller]")]
    public class SourceSubstancesController : BaseViewController<SourceSubstanceViewModel>
    {
        public SourceSubstancesController(IBaseApiService<SourceSubstanceViewModel> apiService)
        {
            _apiService = apiService;
        }

        protected override string CreationTitle => LangResources.Titles.SourceSubstancesCreate;
        protected override string UpdateTitle => LangResources.Titles.SourceSubstancesUpdate;
    }

    [Route("[controller]")]
    public class MethodologyParametersController : BaseViewController<MethodologyParameterViewModel>
    {
        public MethodologyParametersController(IBaseApiService<MethodologyParameterViewModel> apiService)
        {
            _apiService = apiService;
        }

        protected override string CreationTitle => LangResources.Titles.MethodologyParametersCreate;
        protected override string UpdateTitle => LangResources.Titles.MethodologyParametersUpdate;
    }

    [Route("[controller]")]
    public class ConsumptionGroupsController : BaseViewController<ConsumptionGroupViewModel>
    {
        public ConsumptionGroupsController(IBaseApiService<ConsumptionGroupViewModel> apiService)
        {
            _apiService = apiService;
        }

        protected override string CreationTitle => LangResources.Titles.ConsumptionGroupsCreate;
        protected override string UpdateTitle => LangResources.Titles.ConsumptionGroupsUpdate;
    }

    [Route("[controller]")]
    public class SpecificIndicatorsController : BaseViewController<SpecificIndicatorViewModel>
    {
        public SpecificIndicatorsController(IBaseApiService<SpecificIndicatorViewModel> apiService)
        {
            _apiService = apiService;
        }

        protected override string CreationTitle => LangResources.Titles.SpecificIndicatorsCreate;
        protected override string UpdateTitle => LangResources.Titles.SpecificIndicatorsUpdate;
    }

    [Route("[controller]")]
    public class ConsumptionsController : BaseViewController<ConsumptionViewModel>
    {
        public ConsumptionsController(IBaseApiService<ConsumptionViewModel> apiService)
        {
            _apiService = apiService;
        }

        protected override string CreationTitle => LangResources.Titles.ConsumptionsCreate;
        protected override string UpdateTitle => LangResources.Titles.ConsumptionsUpdate;
    }

    [Route("[controller]")]
    public class ParameterValuesController : BaseViewController<ParameterValueViewModel>
    {
        public ParameterValuesController(IBaseApiService<ParameterValueViewModel> apiService)
        {
            _apiService = apiService;
        }

        protected override string CreationTitle => LangResources.Titles.ParameterValuesCreate;
        protected override string UpdateTitle => LangResources.Titles.ParameterValuesUpdate;
    }

    [Route("[controller]")]
    public class GrossEmissionsController : BaseViewController<GrossEmissionViewModel>
    {
        public GrossEmissionsController(IBaseApiService<GrossEmissionViewModel> apiService)
        {
            _apiService = apiService;
        }

        protected override string CreationTitle => LangResources.Titles.GrossEmissionsCreate;
        protected override string UpdateTitle => LangResources.Titles.GrossEmissionsUpdate;
    }

    [Route("[controller]")]
    public class TaxRatesController : BaseViewController<TaxRateViewModel>
    {
        public TaxRatesController(IBaseApiService<TaxRateViewModel> apiService)
        {
            _apiService = apiService;
        }

        protected override string CreationTitle => LangResources.Titles.TaxRatesCreate;
        protected override string UpdateTitle => LangResources.Titles.TaxRatesUpdate;
    }

    [Route("[controller]")]
    public class TaxesController : BaseViewController<TaxViewModel>
    {
        public TaxesController(IBaseApiService<TaxViewModel> apiService)
        {
            _apiService = apiService;
        }

        protected override string CreationTitle => LangResources.Titles.TaxesCreate;
        protected override string UpdateTitle => LangResources.Titles.TaxesUpdate;
    }
}
