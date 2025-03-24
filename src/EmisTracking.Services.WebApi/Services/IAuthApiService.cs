using EmisTracking.WebApi.Models.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace EmisTracking.Services.WebApi.Services
{
    public interface IAuthApiService
    {
        Task<HttpResponseMessage> GetAuthValidateToken(string token);
        Task<HttpResponseMessage> PostChangePasswordAsync(ChangePasswordModel model);
        Task<string> PostSignInAsync(LoginModel model);

        Task GetAuthLogoutAsync();
    }
}
