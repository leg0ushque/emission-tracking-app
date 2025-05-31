using EmisTracking.WebApi.Models.Models;
using EmisTracking.WebApi.Models.ViewModels;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace EmisTracking.Services.WebApi.Services
{
    public interface IConsumptionGroupApiService : IBaseApiService<ConsumptionGroupViewModel>
    {
        public Task<ApiResponseModel<List<ConsumptionGroupViewModel>>> GetByMethodologyIdAsync(
            string methodologyId, bool loadDependencies = false);
    }

    public class ConsumptionGroupApiService : BaseEntityApiService<ConsumptionGroupViewModel>, IConsumptionGroupApiService
    {
        protected override string ControllerPath => "consumptiongroups";

        public ConsumptionGroupApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient(Constants.HttpClientName);
        }

        public Task<ApiResponseModel<List<ConsumptionGroupViewModel>>> GetByMethodologyIdAsync(string methodologyId, bool loadDependencies = false)
        {
            var path = $"{ControllerPath}/byMethodology/{methodologyId}";
            if (loadDependencies)
                path += "?loadDependencies=true";

            return SendRequestAsync<List<ConsumptionGroupViewModel>>(HttpMethod.Get, path);
        }
    }
}
