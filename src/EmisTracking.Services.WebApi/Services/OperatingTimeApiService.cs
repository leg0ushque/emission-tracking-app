using EmisTracking.WebApi.Models.Models;
using EmisTracking.WebApi.Models.ViewModels;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace EmisTracking.Services.WebApi.Services
{
    public interface IOperatingTimeApiService : IBaseApiService<OperatingTimeViewModel>
    {
        public Task<ApiResponseModel<List<OperatingTimeViewModel>>> GetByEmissionSourceIdAsync(
            string id, bool loadDependencies = false);
    }

    public class OperatingTimeApiService : BaseEntityApiService<OperatingTimeViewModel>, IOperatingTimeApiService
    {
        protected override string ControllerPath => "operatingtimes";

        public OperatingTimeApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient(Constants.HttpClientName);
        }

        public Task<ApiResponseModel<List<OperatingTimeViewModel>>> GetByEmissionSourceIdAsync(string id, bool loadDependencies = false)
        {
            var path = $"{ControllerPath}/bySource/{id}";
            if (loadDependencies)
                path += "?loadDependencies=true";

            return SendRequestAsync<List<OperatingTimeViewModel>>(HttpMethod.Get, path);
        }
    }
}
