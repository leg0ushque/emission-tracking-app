using EmisTracking.WebApi.Models.ViewModels;
using System.Net.Http;

namespace EmisTracking.Services.WebApi.Services
{
    public class GrossEmissionApiService : BaseEntityApiService<GrossEmissionViewModel>
    {
        protected override string ControllerPath => "grossemissions";

        public GrossEmissionApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient(Constants.HttpClientName);
        }
    }
}
