using EmisTracking.Localization;
using EmisTracking.Services.Entities;
using EmisTracking.Services.Interfaces;
using EmisTracking.Services.JwtAuth;
using EmisTracking.Services.Services;
using EmisTracking.WebApi.Controllers;
using EmisTracking.WebApi.Models.Models;
using Microsoft.AspNetCore.Identity;
using EmisTracking.Localization;
using EmisTracking.Services.Entities;
using EmisTracking.Services.Interfaces;
using EmisTracking.Services.JwtAuth;
using EmisTracking.Services.Services;
using EmisTracking.WebApi.Models.Models;
using EmisTracking.WebApi.Models.ViewModels;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Constants = EmisTracking.Services.Constants;

namespace EmisTracking.WebApi.Controllers
{
    public abstract class JwtBaseController : ErrorHandlingController
    {
        protected IUsersService _usersService;
        protected IEntityService<Group> _groupsService;
        protected SignInManager<SystemUser> _signInManager;
        protected UserManager<SystemUser> _userManager;
        protected JwtTokenService _jwtTokenService;

        protected async Task<string> CreateUserTokenAsync(User eduUser, SystemUser systemUser)
        {
            var roles = await _userManager.GetRolesAsync(systemUser);
            var role = roles.FirstOrDefault();

            string roleInfo = role;

            return _jwtTokenService.GetToken(systemUser, roles, roleInfo);
        }

        protected async Task<ApiResponseModel<string>> ChangeUserPasswordAsync(string userId, string oldPassword, string newPassword)
        {
            var responseContent = new ApiResponseModel<string>();

            var systemUser = await _userManager.FindByIdAsync(userId);
            if (systemUser == null)
            {
                responseContent.Success = false;
                responseContent.ErrorMessage = string.Format(LangResources.AuthUserNotFoundErrorMessageTemplate, userId);

                return responseContent;
            }

            var eduUser = await _usersService.GetBySystemUserId(systemUser.Id, loadDependencies: true);
            if (eduUser == null)
            {
                responseContent.Success = false;
                responseContent.ErrorMessage = string.Format(LangResources.AuthUserNotFoundErrorMessageTemplate, systemUser.Id);

                return responseContent;
            }

            var result = await _userManager.ChangePasswordAsync(systemUser!, oldPassword, newPassword);

            if (!result.Succeeded)
            {
                responseContent.Success = false;
                responseContent.ErrorMessage = string.Join(". ", result.Errors);

                return responseContent;
            }

            var token = await CreateUserTokenAsync(eduUser, systemUser);

            responseContent.Success = true;
            responseContent.Data = token;

            return responseContent;
        }
    }
}
