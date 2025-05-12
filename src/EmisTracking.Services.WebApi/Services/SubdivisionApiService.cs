using EmisTracking.WebApi.Models.Models;
using EmisTracking.WebApi.Models.ViewModels;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace EmisTracking.Services.WebApi.Services
{
    public class SubdivisionApiService : BaseEntityApiService<SubdivisionViewModel>, ISubdivisionApiService
    {
        protected override string ControllerPath => "subdivisions";

        public SubdivisionApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient(Constants.HttpClientName);
        }

        public Task<ApiResponseModel<List<SubdivisionViewModel>>> GetAllByAreaIdAsync(string areaId)
        {
            return SendRequestAsync<List<SubdivisionViewModel>>(HttpMethod.Get, $"{ControllerPath}/ofArea?areaId={areaId}");
        }
    }
}
