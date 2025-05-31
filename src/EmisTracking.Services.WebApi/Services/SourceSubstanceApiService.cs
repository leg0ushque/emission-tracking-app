using EmisTracking.WebApi.Models.Models;
using EmisTracking.WebApi.Models.ViewModels;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace EmisTracking.Services.WebApi.Services
{
    public interface ISourceSubstanceApiService : IBaseApiService<SourceSubstanceViewModel>
    {
        Task<ApiResponseModel<List<SourceSubstanceViewModel>>> GetAllByEmissionSourceIdAsync(string emissionSourceId,
            bool loadDependencies = false);
    }

    public class SourceSubstanceApiService : BaseEntityApiService<SourceSubstanceViewModel>, ISourceSubstanceApiService
    {
        protected override string ControllerPath => "sourcesubstances";

        public SourceSubstanceApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient(Constants.HttpClientName);
        }

        public Task<ApiResponseModel<List<SourceSubstanceViewModel>>> GetAllByEmissionSourceIdAsync(string emissionSourceId,
            bool loadDependencies = false)
        {
            var path = $"{ControllerPath}/byEmissionSource/{emissionSourceId}";

            if (loadDependencies)
                path += "?loadDependencies=true";

            return SendRequestAsync<List<SourceSubstanceViewModel>>(HttpMethod.Get, path);
        }
    }
}
