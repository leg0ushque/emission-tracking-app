using EmisTracking.WebApi.Models.Models;
using EmisTracking.WebApi.Models.ViewModels;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace EmisTracking.Services.WebApi.Services
{
    public interface IModeApiService : IBaseApiService<ModeViewModel>
    {
        public Task<ApiResponseModel<List<ModeViewModel>>> GetAllWithMethodologiesAsync();
    }

    public class ModeApiService : BaseEntityApiService<ModeViewModel>, IModeApiService
    {
        protected override string ControllerPath => "modes";

        public ModeApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient(Constants.HttpClientName);
        }

        public Task<ApiResponseModel<List<ModeViewModel>>> GetAllWithMethodologiesAsync()
        {
            var path = $"{ControllerPath}/withMethodologies";

            return SendRequestAsync<List<ModeViewModel>>(HttpMethod.Get, path);
        }
    }
}
