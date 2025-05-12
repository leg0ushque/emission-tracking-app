using EmisTracking.WebApi.Models.ViewModels;
using System.Net.Http;

namespace EmisTracking.Services.WebApi.Services
{
    public class MethodologyParameterApiService : BaseEntityApiService<MethodologyParameterViewModel>
    {
        protected override string ControllerPath => "methodologyparameters";

        public MethodologyParameterApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient(Constants.HttpClientName);
        }
    }
}
