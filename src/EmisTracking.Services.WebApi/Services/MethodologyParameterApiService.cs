using EmisTracking.WebApi.Models.Models;
using EmisTracking.WebApi.Models.ViewModels;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace EmisTracking.Services.WebApi.Services
{
    public interface IMethodologyParameterApiService : IBaseApiService<MethodologyParameterViewModel>
    {
        public Task<ApiResponseModel<List<MethodologyParameterViewModel>>> GetByMethodologyIdAsync(
            string methodologyId, bool loadDependencies = false);
    }

    public class MethodologyParameterApiService : BaseEntityApiService<MethodologyParameterViewModel>, IMethodologyParameterApiService
    {
        protected override string ControllerPath => "methodologyparameters";

        public MethodologyParameterApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient(Constants.HttpClientName);
        }

        public Task<ApiResponseModel<List<MethodologyParameterViewModel>>> GetByMethodologyIdAsync(string methodologyId, bool loadDependencies = false)
        {
            var path = $"{ControllerPath}/byMethodology/{methodologyId}";
            if (loadDependencies)
                path += "?loadDependencies=true";

            return SendRequestAsync<List<MethodologyParameterViewModel>>(HttpMethod.Get, path);
        }
    }
}
