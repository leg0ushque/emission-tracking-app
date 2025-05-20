using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using EmisTracking.WebApi.Models.Models;
using EmisTracking.WebApi.Models.ViewModels;

namespace EmisTracking.Services.WebApi.Services
{
    public abstract class BaseEntityApiService<TEntityViewModel> : BaseApiService, IBaseApiService<TEntityViewModel>
        where TEntityViewModel : class, IViewModel, new()
    {
        protected abstract string ControllerPath { get; }

        public virtual Task<ApiResponseModel<string>> CreateAsync(TEntityViewModel item)
        {
            return SendRequestAsync<string>(HttpMethod.Post, $"{ControllerPath}", item);
        }

        public Task<ApiResponseModel<List<TEntityViewModel>>> GetAllAsync(bool loadDependencies = false)
        {
            var path = $"{ControllerPath}";

            if (loadDependencies)
                path += "?loadDependencies=true";

            return SendRequestAsync<List<TEntityViewModel>>(HttpMethod.Get, path);
        }

        public virtual Task<ApiResponseModel<TEntityViewModel>> GetByIdAsync(string id, bool loadDependencies = false)
        {
            var path = $"{ControllerPath}/{id}";
            if (loadDependencies)
                path += "?loadDependencies=true";

            return SendRequestAsync<TEntityViewModel>(HttpMethod.Get, path);
        }

        public virtual Task<ApiResponseModel<object>> UpdateAsync(TEntityViewModel item)
        {
            return SendRequestAsync<object>(HttpMethod.Put, $"{ControllerPath}", item);
        }

        public virtual Task<ApiResponseModel<object>> DeleteByIdAsync(string id)
        {
            return SendRequestAsync<object>(HttpMethod.Delete, $"{ControllerPath}/{id}");
        }
    }
}
