using EmisTracking.WebApi.Models.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace EmisTracking.Services.WebApi.Services
{
    public interface IAuthApiService
    {
        Task<ApiResponseModel<string>> PostRegister(RegisterModel model);
        Task<ApiResponseModel<object>> GetAuthValidateToken(string token);
        Task<ApiResponseModel<string>> PostChangePasswordAsync(ChangePasswordModel model);
        Task<ApiResponseModel<string>> PostSignInAsync(LoginModel model);

        Task<ApiResponseModel<object>> GetAuthLogoutAsync();
    }
}
