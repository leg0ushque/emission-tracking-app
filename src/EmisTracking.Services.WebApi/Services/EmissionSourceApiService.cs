using EmisTracking.WebApi.Models.Models;
using EmisTracking.WebApi.Models.ViewModels;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace EmisTracking.Services.WebApi.Services
{
    public class EmissionSourceApiService : BaseEntityApiService<EmissionSourceViewModel>, IEmissionSourceApiService
    {
        protected override string ControllerPath => "emissionsources";

        public EmissionSourceApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient(Constants.HttpClientName);
        }

        public Task<ApiResponseModel<List<EmissionSourceViewModel>>> GetAllBySubdivisionAsync(string subdivisionId, bool loadDependencies = false)
        {
            var path = $"{ControllerPath}/bySubdivision/{subdivisionId}";

            if (loadDependencies)
                path += "?loadDependencies=true";

            return SendRequestAsync<List<EmissionSourceViewModel>>(HttpMethod.Get, path);
        }
    }
}
