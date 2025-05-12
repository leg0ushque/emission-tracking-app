using EmisTracking.WebApi.Models.ViewModels;
using System.Net.Http;

namespace EmisTracking.Services.WebApi.Services
{
    public class EmissionSourceApiService : BaseEntityApiService<EmissionSourceViewModel>
    {
        protected override string ControllerPath => "emissionsources";

        public EmissionSourceApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient(Constants.HttpClientName);
        }
    }
}
