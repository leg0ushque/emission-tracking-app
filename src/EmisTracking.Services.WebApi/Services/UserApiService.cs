using EmisTracking.WebApi.Models.Models;
using EmisTracking.WebApi.Models.ViewModels;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace EmisTracking.Services.WebApi.Services
{
    public interface IUserApiService : IBaseApiService<UserViewModel>
    {
        Task<ApiResponseModel<string>> PostRegisterAsync(RegisterModel model);
        Task<ApiResponseModel<List<UserViewModel>>> GetByRoleAsync(string roleName);
    }

    public class UserApiService : BaseEntityApiService<UserViewModel>, IUserApiService
    {
        protected override string ControllerPath => "users";

        public UserApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient(Constants.HttpClientName);
        }
        public Task<ApiResponseModel<string>> PostRegisterAsync(RegisterModel model)
        {
            return SendRequestAsync<string>(HttpMethod.Post, $"users/register", model);
        }

        public Task<ApiResponseModel<List<UserViewModel>>> GetByRoleAsync(string roleName)
        {
            return SendRequestAsync<List<UserViewModel>>(HttpMethod.Get, $"users/byRole/{roleName}");
        }
    }
}
