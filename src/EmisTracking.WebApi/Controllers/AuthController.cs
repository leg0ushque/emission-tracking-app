using EmisTracking.Localization.StudentsPerf.Localization;
using EmisTracking.Services;
using EmisTracking.Services.Entities;
using EmisTracking.Services.JwtAuth;
using EmisTracking.Services.Services;
using EmisTracking.WebApi.Models.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EmisTracking.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class AuthController(
        IUsersService usersService,
            SignInManager<SystemUser> signInManager,
            UserManager<SystemUser> userManager,
        JwtTokenService jwtTokenService) : ErrorHandlingController
    {
        private readonly IUsersService _usersService = usersService;
        private readonly SignInManager<SystemUser> _signInManager = signInManager;
        private readonly UserManager<SystemUser> _userManager = userManager;
        private readonly JwtTokenService _jwtTokenService = jwtTokenService;

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var systemUser = new SystemUser()
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = model.FirstName,
                MiddleName = model.MiddleName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.Email,
                NormalizedUserName = model.Email,
                NormalizedEmail = model.Email,
                EmailConfirmed = true,
                LockoutEnabled = true,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };

            var creationResult = await CreateUserAsync(systemUser, model.Password);

            return creationResult.Success ? Ok(creationResult) : BadRequest(creationResult);
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (model == null)
            { ModelState.AddModelError("", LangResources.MustBeFilledMessage); }

            if (!ModelState.IsValid)
            {
                IActionResult result = CreateBadRequestResponse(ModelState);

                return result;
            }

            var loginResult = await LoginUserAsync(model!.Email, model.Password);

            return loginResult.Success ? Ok(loginResult) : BadRequest(loginResult);
        }

        [HttpPost("changePassword")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordModel model)
        {
            if (model == null)
            { ModelState.AddModelError("", "Empty request body received"); }

            if (!ModelState.IsValid)
            {
                IActionResult result = CreateBadRequestResponse(ModelState);

                return result;
            }

            var changePasswordResponse = await ChangeCurrentUserPasswordAsync(model!.OldPassword, model.NewPassword);

            return changePasswordResponse.Success ? Ok(changePasswordResponse) : BadRequest(changePasswordResponse);
        }

        [Authorize]
        [HttpGet("logout")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return Ok(new ApiResponseModel<object>() { Success = true });
        }

        [HttpPost("validate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public IActionResult Validate([FromBody] string token)
        {
            var tokenIsValid = _jwtTokenService.ValidateToken(token);

            var response = new ApiResponseModel<object>() { Success = tokenIsValid };

            return response.Success ? Ok(response) : Unauthorized(response);
        }

        private async Task<ApiResponseModel<string>> ChangeCurrentUserPasswordAsync(string oldPassword, string newPassword)
        {
            var responseContent = new ApiResponseModel<string>();

            var userId = HttpContext.User.FindFirst(Constants.UserIdClaimType)!.Value;

            var systemUser = await _userManager.FindByIdAsync(userId);
            if (systemUser == null)
            {
                responseContent.Success = false;
                responseContent.ErrorMessage = string.Format(LangResources.AuthUserNotFoundErrorMessageTemplate, userId);

                return responseContent;
            }

            // FIXME
            var emissionUser = await _usersService.GetBySystemUserId(systemUser.Id);
            if (emissionUser == null)
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

            var token = await CreateUserTokenAsync(emissionUser, systemUser);

            responseContent.Success = true;
            responseContent.Data = token;

            return responseContent;
        }

        private async Task<ApiResponseModel<string>> LoginUserAsync(string login, string password)
        {
            var responseContent = new ApiResponseModel<string>();

            var signInResult = await _signInManager.PasswordSignInAsync(login, password, isPersistent: false, lockoutOnFailure: false);

            if (signInResult.Succeeded)
            {
                var systemUser = await _userManager.FindByNameAsync(login);

                if (systemUser == null)
                {
                    responseContent.Success = false;
                    responseContent.ErrorMessage = string.Format(LangResources.AuthUserNotFoundErrorMessageTemplate, login);

                    return responseContent;
                }

                var eduUser = await _usersService.GetBySystemUserId(systemUser.Id);

                if (eduUser == null && systemUser.Email != Services.Database.Constants.AdminMailbox)
                {
                    responseContent.Success = false;
                    responseContent.ErrorMessage = string.Format(LangResources.AuthUserNotFoundErrorMessageTemplate, systemUser.Id);

                    return responseContent;
                }

                var token = await CreateUserTokenAsync(eduUser, systemUser);

                responseContent.Success = true;
                responseContent.Data = token;

                return responseContent;
            }

            responseContent.Success = false;
            responseContent.ErrorMessage = LangResources.SignInFailedErrorMessage;

            return responseContent;
        }

        private async Task<ApiResponseModel<string>> CreateUserAsync(SystemUser systemUser, string password)
        {
            var result = new ApiResponseModel<string>();

            var hasher = new PasswordHasher<SystemUser>();
            systemUser.PasswordHash = hasher.HashPassword(systemUser, password);

            var creationResult = await _userManager.CreateAsync(systemUser, password);

            if (creationResult.Succeeded)
            {
                await _userManager.AddToRoleAsync(systemUser, Constants.EditorRole);

                var eduUser = new User
                {
                    SystemUserId = systemUser.Id,
                };

                eduUser.Id = await _usersService.AddAsync(eduUser);

                var token = await CreateUserTokenAsync(eduUser, systemUser);

                result.Success = true;
                result.Data = token;

                return result;
            }

            var messages = creationResult.Errors.Select(x => x.Description).ToArray();


            result.Success = false;
            result.ErrorMessage = string.Join(". ", messages);

            return result;
        }

        private async Task<string> CreateUserTokenAsync(User emissionUser, SystemUser systemUser)
        {
            var roles = await _userManager.GetRolesAsync(systemUser);
            var role = roles.FirstOrDefault();

            string roleInfo = Constants.AdminRole;

            if (role == Constants.EditorRole)
            {
                roleInfo = "Редактор"; // FIXME
            }
            // FIXME Добавить другие роли

            return _jwtTokenService.GetToken(systemUser, roles, roleInfo);
        }
    }
}
