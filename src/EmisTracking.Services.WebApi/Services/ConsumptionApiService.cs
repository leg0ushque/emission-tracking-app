using EmisTracking.WebApi.Models.Models;
using EmisTracking.WebApi.Models.ViewModels;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace EmisTracking.Services.WebApi.Services
{
    public interface IConsumptionApiService : IBaseApiService<ConsumptionViewModel>
    {
        public Task<ApiResponseModel<List<ConsumptionViewModel>>> GetByConsumptionGroupIdAsync(string id,
            bool loadDependencies = false);
    }

    public class ConsumptionApiService : BaseEntityApiService<ConsumptionViewModel>, IConsumptionApiService
    {
        protected override string ControllerPath => "consumptions";

        public ConsumptionApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient(Constants.HttpClientName);
        }

        public Task<ApiResponseModel<List<ConsumptionViewModel>>> GetByConsumptionGroupIdAsync(string id,
            bool loadDependencies = false)
        {
            var path = $"{ControllerPath}/byConsumptionGroup/{id}";
            if (loadDependencies)
                path += "?loadDependencies=true";

            return SendRequestAsync<List<ConsumptionViewModel>>(HttpMethod.Get, path);
        }
    }
}
