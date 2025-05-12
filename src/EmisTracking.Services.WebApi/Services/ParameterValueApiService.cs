using EmisTracking.WebApi.Models.ViewModels;
using System.Net.Http;

namespace EmisTracking.Services.WebApi.Services
{
    public class ParameterValueApiService : BaseEntityApiService<ParameterValueViewModel>
    {
        protected override string ControllerPath => "parametervalues";

        public ParameterValueApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient(Constants.HttpClientName);
        }
    }
}
