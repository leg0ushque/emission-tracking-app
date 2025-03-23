using EmisTracking.Localization;
using EmisTracking.Services;
using EmisTracking.Services.Entities;
using EmisTracking.Services.Interfaces;
using EmisTracking.Services.JwtAuth;
using EmisTracking.WebApi.Models.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace EmisTracking.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class AuthController(
        IEntityService<User> usersService,
        SignInManager<SystemUser> signInManager,
        UserManager<SystemUser> userManager,
        JwtTokenService jwtTokenService) : ErrorHandlingController
    {
        private readonly IEntityService<User> _usersService = usersService;
        private readonly SignInManager<SystemUser> _signInManager = signInManager;
        private readonly UserManager<SystemUser> _userManager = userManager;
        private readonly JwtTokenService _jwtTokenService = jwtTokenService;

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if(model == null)
            { ModelState.AddModelError("", LangResources.ItemCannotBeNullMessage); }

            if (!ModelState.IsValid)
            {
                IActionResult result = CreateBadRequestResponse(ModelState);
                return Task.FromResult(result);
            }

            return LoginUserAsync(model!.Email, model.Password);
        }

        [HttpPost("changePassword")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public Task<IActionResult> ChangePassword([FromBody] ChangePasswordModel model)
        {
            if (model == null)
            { ModelState.AddModelError("", "Empty request body received"); }

            if (!ModelState.IsValid)
            {
                IActionResult result = CreateBadRequestResponse(ModelState);
                return Task.FromResult(result);
            }

            return ChangeCurrentUserPasswordAsync(model!.OldPassword, model.NewPassword);
        }

        [Authorize]
        [HttpGet("logout")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return Ok();
        }

        [HttpPost("validate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public IActionResult Validate([FromBody] string token)
        {
            return _jwtTokenService.ValidateToken(token) ? Ok() : Forbid();
        }

        private async Task<IActionResult> ChangeCurrentUserPasswordAsync(string oldPassword, string newPassword)
        {
            var userId = HttpContext.User.FindFirst(Constants.UserIdClaimType)!.Value;

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return Forbid();

            var result = await _userManager.ChangePasswordAsync(user!, oldPassword, newPassword);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            var token = await CreateUserTokenAsync(user);

            return Ok(token);
        }

        private async Task<IActionResult> LoginUserAsync(string login, string password)
        {
            var result = await _signInManager.PasswordSignInAsync(login, password, isPersistent: false, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(login);

                if (user == null)
                    return Forbid();

                var token = await CreateUserTokenAsync(user);

                return Ok(token);
            }

            return Forbid();
        }

        private async Task<string> CreateUserTokenAsync(SystemUser systemUser)
        {
            var roles = await _userManager.GetRolesAsync(systemUser);
            var role = roles.FirstOrDefault();

            var appUser = await _usersService.GetByIdAsync(systemUser.Id);

            string roleInfo = Constants.AdminRole;

            if (role == Constants.EngineerRole)
            {
                roleInfo = appUser.Info;
            }

            return _jwtTokenService.GetToken(systemUser, roles, roleInfo);
        }
    }
}
