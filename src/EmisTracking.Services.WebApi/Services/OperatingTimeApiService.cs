using EmisTracking.WebApi.Models.ViewModels;
using System.Net.Http;

namespace EmisTracking.Services.WebApi.Services
{
    public class OperatingTimeApiService : BaseEntityApiService<OperatingTimeViewModel>
    {
        protected override string ControllerPath => "operatingtimes";

        public OperatingTimeApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient(Constants.HttpClientName);
        }
    }
}
