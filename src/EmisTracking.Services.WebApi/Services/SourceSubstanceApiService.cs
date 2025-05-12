using EmisTracking.WebApi.Models.ViewModels;
using System.Net.Http;

namespace EmisTracking.Services.WebApi.Services
{
    public class SourceSubstanceApiService : BaseEntityApiService<SourceSubstanceViewModel>
    {
        protected override string ControllerPath => "sourcesubstances";

        public SourceSubstanceApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient(Constants.HttpClientName);
        }
    }
}
