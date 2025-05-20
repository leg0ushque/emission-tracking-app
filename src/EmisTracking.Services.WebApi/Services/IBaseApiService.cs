using System.Collections.Generic;
using System.Threading.Tasks;
using EmisTracking.WebApi.Models.Models;
using EmisTracking.WebApi.Models.ViewModels;

namespace EmisTracking.Services.WebApi.Services
{
    public interface IBaseApiService<TEntityViewModel>
        where TEntityViewModel : class, IViewModel, new()
    {

        public Task<ApiResponseModel<string>> CreateAsync(TEntityViewModel item);

        public Task<ApiResponseModel<List<TEntityViewModel>>> GetAllAsync(bool loadDependencies = false);

        public Task<ApiResponseModel<TEntityViewModel>> GetByIdAsync(string id, bool loadDependencies = false);

        public Task<ApiResponseModel<object>> UpdateAsync(TEntityViewModel item);

        public Task<ApiResponseModel<object>> DeleteByIdAsync(string id);
    }
}
