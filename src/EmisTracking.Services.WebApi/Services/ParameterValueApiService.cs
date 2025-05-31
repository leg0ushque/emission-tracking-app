using EmisTracking.WebApi.Models.Models;
using EmisTracking.WebApi.Models.ViewModels;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace EmisTracking.Services.WebApi.Services
{
    public interface IParameterValueApiService : IBaseApiService<ParameterValueViewModel>
    {
        public Task<ApiResponseModel<List<ParameterValueViewModel>>> GetByParameterIdAsync(
            string id, bool loadDependencies = false);
    }

    public class ParameterValueApiService : BaseEntityApiService<ParameterValueViewModel>, IParameterValueApiService
    {
        protected override string ControllerPath => "parametervalues";

        public ParameterValueApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient(Constants.HttpClientName);
        }

        public Task<ApiResponseModel<List<ParameterValueViewModel>>> GetByParameterIdAsync(string id, bool loadDependencies = false)
        {
            var path = $"{ControllerPath}/byParameter/{id}";
            if (loadDependencies)
                path += "?loadDependencies=true";

            return SendRequestAsync<List<ParameterValueViewModel>>(HttpMethod.Get, path);
        }
    }
}
