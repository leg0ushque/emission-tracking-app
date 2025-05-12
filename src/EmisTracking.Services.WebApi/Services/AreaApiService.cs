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
}
