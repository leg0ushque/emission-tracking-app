using EmisTracking.WebApi.Models.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace EmisTracking.Services.WebApi.Services
{
    public interface IAuthApiService
    {
        Task<ApiResponseModel<string>> PostRegister(RegisterModel model);
        Task<ApiResponseModel<object>> GetAuthValidateToken(string token);
        Task<ApiResponseModel<string>> PostChangePassword(ChangePasswordModel model);
        Task<ApiResponseModel<string>> PostSignIn(LoginModel model);

        Task<ApiResponseModel<object>> GetAuthLogout();
        Task<ApiResponseModel<object>> GetPing();

    }

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

        public Task<ApiResponseModel<string>> PostSignIn(LoginModel model)
        {
            return SendRequestAsync<string>(HttpMethod.Post, $"auth/login", model);
        }

        public Task<ApiResponseModel<string>> PostChangePassword(ChangePasswordModel model)
        {
            return SendRequestAsync<string>(HttpMethod.Post, $"auth/changepassword", model);
        }

        public Task<ApiResponseModel<object>> GetAuthValidateToken(string token)
        {
            return SendRequestAsync<object>(HttpMethod.Post, $"auth/validate", token);
        }

        public Task<ApiResponseModel<object>> GetAuthLogout()
        {
            return SendRequestAsync<object>(HttpMethod.Get, $"auth/logout");
        }

        public Task<ApiResponseModel<object>> GetPing()
        {
            return SendRequestAsync<object>(HttpMethod.Get, $"auth/ping");
        }
    }
}
