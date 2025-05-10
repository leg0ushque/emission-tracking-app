using EmisTracking.WebApi.Models.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace EmisTracking.Services.WebApi.Services
{
    public class AuthApiService : BaseApiService, IAuthApiService
    {
        public AuthApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient(Constants.HttpClientName);
        }

        public Task<ApiResponseModel<string>> PostRegister(RegisterModel model)
        {
            return SendRequestAsync<string>(HttpMethod.Post, $"auth/register", model);
        }

        public Task<ApiResponseModel<string>> PostSignInAsync(LoginModel model)
        {
            return SendRequestAsync<string>(HttpMethod.Post, $"auth/login", model);
        }

        public Task<ApiResponseModel<string>> PostChangePasswordAsync(ChangePasswordModel model)
        {
            return SendRequestAsync<string>(HttpMethod.Post, $"auth/changepassword", model);
        }

        public Task<ApiResponseModel<object>> GetAuthValidateToken(string token)
        {
            return SendRequestAsync<object>(HttpMethod.Post, $"auth/validate", token);
        }

        public Task<ApiResponseModel<object>> GetAuthLogoutAsync()
        {
            return SendRequestAsync<object>(HttpMethod.Get, $"auth/logout");
        }
    }
}
