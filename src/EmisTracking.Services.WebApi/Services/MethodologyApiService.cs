using EmisTracking.WebApi.Models.ViewModels;
using System.Net.Http;

namespace EmisTracking.Services.WebApi.Services
{
    public class MethodologyApiService : BaseEntityApiService<MethodologyViewModel>
    {
        protected override string ControllerPath => "methodologies";

        public MethodologyApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient(Constants.HttpClientName);
        }
    }
}
