using EmisTracking.WebApi.Models.Models;
using EmisTracking.WebApi.Models.ViewModels;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace EmisTracking.Services.WebApi.Services
{
    public interface ISpecificIndicatorApiService : IBaseApiService<SpecificIndicatorViewModel>
    {
        Task<ApiResponseModel<List<SpecificIndicatorViewModel>>> GetByConsumptionGroupIdAsync(
            string consumptionGroupId, bool loadDependencies = false);
    }

    public class SpecificIndicatorApiService : BaseEntityApiService<SpecificIndicatorViewModel>, ISpecificIndicatorApiService
    {
        protected override string ControllerPath => "specificindicators";

        public SpecificIndicatorApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient(Constants.HttpClientName);
        }

        public Task<ApiResponseModel<List<SpecificIndicatorViewModel>>> GetByConsumptionGroupIdAsync(
            string consumptionGroupId, bool loadDependencies = false)
        {
            var path = $"{ControllerPath}/byConsumptionGroup/{consumptionGroupId}";
            if (loadDependencies)
                path += "?loadDependencies=true";

            return SendRequestAsync<List<SpecificIndicatorViewModel>>(HttpMethod.Get, path);
        }
    }
}
