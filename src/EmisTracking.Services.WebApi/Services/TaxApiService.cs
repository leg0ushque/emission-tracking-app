using EmisTracking.WebApi.Models.ViewModels;
using System.Net.Http;

namespace EmisTracking.Services.WebApi.Services
{
    public class TaxApiService : BaseEntityApiService<TaxViewModel>
    {
        protected override string ControllerPath => "taxes";

        public TaxApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient(Constants.HttpClientName);
        }
    }
}
