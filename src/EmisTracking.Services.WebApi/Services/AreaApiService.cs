using EmisTracking.WebApi.Models.ViewModels;
using System.Net.Http;

namespace EmisTracking.Services.WebApi.Services
{
    public class AreaApiService : BaseEntityApiService<AreaViewModel>
    {
        protected override string ControllerPath => "areas";

        public AreaApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient(Constants.HttpClientName);
        }
    }

    public class SubdivisionApiService : BaseEntityApiService<SubdivisionViewModel>
    {
        protected override string ControllerPath => "subdivisions";

        public SubdivisionApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient(Constants.HttpClientName);
        }
    }

    public class ModeApiService : BaseEntityApiService<ModeViewModel>
    {
        protected override string ControllerPath => "modes";

        public ModeApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient(Constants.HttpClientName);
        }
    }

    public class MethodologyApiService : BaseEntityApiService<MethodologyViewModel>
    {
        protected override string ControllerPath => "methodologies";

        public MethodologyApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient(Constants.HttpClientName);
        }
    }

    public class EmissionSourceApiService : BaseEntityApiService<EmissionSourceViewModel>
    {
        protected override string ControllerPath => "emissionsources";

        public EmissionSourceApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient(Constants.HttpClientName);
        }
    }

    public class OperatingTimeApiService : BaseEntityApiService<OperatingTimeViewModel>
    {
        protected override string ControllerPath => "operatingtimes";

        public OperatingTimeApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient(Constants.HttpClientName);
        }
    }

    public class PollutantApiService : BaseEntityApiService<PollutantViewModel>
    {
        protected override string ControllerPath => "pollutants";

        public PollutantApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient(Constants.HttpClientName);
        }
    }

    public class SourceSubstanceApiService : BaseEntityApiService<SourceSubstanceViewModel>
    {
        protected override string ControllerPath => "sourcesubstances";

        public SourceSubstanceApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient(Constants.HttpClientName);
        }
    }

    public class MethodologyParameterApiService : BaseEntityApiService<MethodologyParameterViewModel>
    {
        protected override string ControllerPath => "methodologyparameters";

        public MethodologyParameterApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient(Constants.HttpClientName);
        }
    }

    public class ConsumptionGroupApiService : BaseEntityApiService<ConsumptionGroupViewModel>
    {
        protected override string ControllerPath => "consumptiongroups";

        public ConsumptionGroupApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient(Constants.HttpClientName);
        }
    }

    public class SpecificIndicatorApiService : BaseEntityApiService<SpecificIndicatorViewModel>
    {
        protected override string ControllerPath => "specificindicators";

        public SpecificIndicatorApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient(Constants.HttpClientName);
        }
    }

    public class ConsumptionApiService : BaseEntityApiService<ConsumptionViewModel>
    {
        protected override string ControllerPath => "consumptions";

        public ConsumptionApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient(Constants.HttpClientName);
        }
    }

    public class ParameterValueApiService : BaseEntityApiService<ParameterValueViewModel>
    {
        protected override string ControllerPath => "parametervalues";

        public ParameterValueApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient(Constants.HttpClientName);
        }
    }

    public class GrossEmissionApiService : BaseEntityApiService<GrossEmissionViewModel>
    {
        protected override string ControllerPath => "grossemissions";

        public GrossEmissionApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient(Constants.HttpClientName);
        }
    }

    public class TaxRateApiService : BaseEntityApiService<TaxRateViewModel>
    {
        protected override string ControllerPath => "taxrates";

        public TaxRateApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient(Constants.HttpClientName);
        }
    }

    public class TaxApiService : BaseEntityApiService<TaxViewModel>
    {
        protected override string ControllerPath => "taxes";

        public TaxApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient(Constants.HttpClientName);
        }
    }
}
