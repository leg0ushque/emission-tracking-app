using EmisTracking.WebApi.Models.Models;
using EmisTracking.WebApi.Models.ViewModels;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace EmisTracking.Services.WebApi.Services
{
    public class OperatingTimeApiService : BaseEntityApiService<OperatingTimeViewModel>
    {
        protected override string ControllerPath => "operatingtimes";

        public OperatingTimeApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient(Constants.HttpClientName);
        }

        public Task<ApiResponseModel<List<EmissionSourceViewModel>>> GetAllByEmissionSourceAsync(string emissionSourceId, bool loadDependencies = false)
        {
            var path = $"{ControllerPath}/byEmissionSource/{emissionSourceId}";

            if (loadDependencies)
                path += "?loadDependencies=true";

            return SendRequestAsync<List<EmissionSourceViewModel>>(HttpMethod.Get, path);
        }
    }
}
