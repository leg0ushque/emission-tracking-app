using EmisTracking.WebApi.Models.ViewModels;
using System.Net.Http;

namespace EmisTracking.Services.WebApi.Services
{
    public class TaxRateApiService : BaseEntityApiService<TaxRateViewModel>
    {
        protected override string ControllerPath => "taxrates";

        public TaxRateApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient(Constants.HttpClientName);
        }
    }
}
