using EmisTracking.WebApi.Models.ViewModels;
using System.Net.Http;

namespace EmisTracking.Services.WebApi.Services
{
    public class SpecificIndicatorApiService : BaseEntityApiService<SpecificIndicatorViewModel>
    {
        protected override string ControllerPath => "specificindicators";

        public SpecificIndicatorApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient(Constants.HttpClientName);
        }
    }
}
