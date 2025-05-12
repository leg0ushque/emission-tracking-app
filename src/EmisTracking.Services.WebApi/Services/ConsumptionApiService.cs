using EmisTracking.WebApi.Models.ViewModels;
using System.Net.Http;

namespace EmisTracking.Services.WebApi.Services
{
    public class ConsumptionApiService : BaseEntityApiService<ConsumptionViewModel>
    {
        protected override string ControllerPath => "consumptions";

        public ConsumptionApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient(Constants.HttpClientName);
        }
    }
}
