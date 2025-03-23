using EmisTracking.Services.WebApi.Helpers;
using EmisTracking.WebApi.Models.Models;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace EmisTracking.Services.WebApi.Services
{
    public class AuthApiService(IHttpClientFactory httpClientFactory) : IAuthApiService
    {
        private readonly HttpClient _httpClient = httpClientFactory.CreateClient(Constants.HttpClientName);

        public async Task<string> PostSignInAsync(LoginModel model)
        {
            using var content = JsonHelper.ObjectToStringContent(model);
            var response = await _httpClient.PostAsync("auth/login", content);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> PostChangePasswordAsync(ChangePasswordModel model)
        {
            using var content = JsonHelper.ObjectToStringContent(model);
            var response = await _httpClient.PostAsync("auth/changepassword", content);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<HttpResponseMessage> GetAuthValidateToken(string token)
        {
            using var content = JsonHelper.ObjectToStringContent(token);
            var result = await _httpClient.PostAsync("auth/validate", content);
            return result;
        }

        public async Task GetAuthLogoutAsync()
        {
            var result = await _httpClient.GetAsync("auth/logout");

            result.EnsureSuccessStatusCode();
        }
    }
}
