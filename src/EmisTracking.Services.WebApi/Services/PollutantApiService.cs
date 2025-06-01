using EmisTracking.WebApi.Models.ViewModels;
using System.Net.Http;

namespace EmisTracking.Services.WebApi.Services
{
    public class PollutantApiService : BaseEntityApiService<PollutantViewModel>
    {
        protected override string ControllerPath => "pollutants";

        public PollutantApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient(Constants.HttpClientName);
        }
    }
}
