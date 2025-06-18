using EmisTracking.WebApi.Models.Models;
using EmisTracking.WebApi.Models.ViewModels;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace EmisTracking.Services.WebApi.Services
{
    public interface IGrossEmissionApiService : IBaseApiService<GrossEmissionViewModel>
    {
        public Task<ApiResponseModel<List<GrossEmissionViewModel>>> Calculate(CalculationFormViewModel model);
        public Task<ApiResponseModel<CalculationCheckResultViewModel>> СheckCalculation(CalculationFormViewModel model);
    }

    public class GrossEmissionApiService : BaseEntityApiService<GrossEmissionViewModel>, IGrossEmissionApiService
    {
        protected override string ControllerPath => "grossemissions";

        public GrossEmissionApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient(Constants.HttpClientName);
        }

        public Task<ApiResponseModel<List<GrossEmissionViewModel>>> Calculate(CalculationFormViewModel model)
        {
            return SendRequestAsync<List<GrossEmissionViewModel>>(HttpMethod.Post, $"{ControllerPath}/calculate", model);
        }

        public Task<ApiResponseModel<CalculationCheckResultViewModel>> СheckCalculation(CalculationFormViewModel model)
        {
            return SendRequestAsync<CalculationCheckResultViewModel>(HttpMethod.Post, $"{ControllerPath}/checkCalculation", model);
        }
    }
}
