using EmisTracking.WebApi.Models.ViewModels;
using EmisTracking.WebApi.Models.Models;

namespace EmisTracking.LoadTesting
{
    public interface ILoadTestingHttpClient<TEntityViewModel>
        where TEntityViewModel : IViewModel
    {
        public Task<ApiResponseModel<List<TEntityViewModel>>> GetAllAsync(string token, bool loadDependencies = false);
    }
}
