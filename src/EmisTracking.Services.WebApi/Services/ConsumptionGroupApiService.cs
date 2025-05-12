using EmisTracking.WebApi.Models.ViewModels;
using System.Net.Http;

namespace EmisTracking.Services.WebApi.Services
{
    public class ConsumptionGroupApiService : BaseEntityApiService<ConsumptionGroupViewModel>
    {
        protected override string ControllerPath => "consumptiongroups";

        public ConsumptionGroupApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient(Constants.HttpClientName);
        }
    }
}
