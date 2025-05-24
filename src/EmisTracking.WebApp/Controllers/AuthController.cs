using EmisTracking.Services.WebApi.Services;
using EmisTracking.WebApi.Models.Models;
using EmisTracking.WebApp.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Threading.Tasks;

namespace EmisTracking.WebApp.Controllers
{
    [Route("[controller]")]
    public class AuthController(IAuthApiService authApiService) : Controller
    {
        private readonly IAuthApiService authApiService = authApiService;

        [HttpGet("register")]
        public IActionResult Register()
        {
            var model = new RegisterModel();

            return View(model);
        }

        [HttpPost("register")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([FromForm] RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var response = await authApiService.PostRegister(model);

            if (response.Success)
            {
                SetTokenCookies(response.Data);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                UpdateModelStateErrors(ModelState, response.Errors, response.ErrorMessage);

                return View(model);
            }
        }

        [AllowAnonymous]
        [HttpGet("login")]
        [LoadLayoutDataFilter]
        public IActionResult Login()
        {
            var model = new LoginModel();

            return View(model);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([FromForm] LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var response = await authApiService.PostSignInAsync(model);

            if (response.Success)
            {
                SetTokenCookies(response.Data);

                return RedirectToAction("Index", "Home");

            }
            else
            {
                UpdateModelStateErrors(ModelState, response.Errors, response.ErrorMessage);

                return View(model);
            }
        }

        [Authorize]
        [LoadLayoutDataFilter]
        [HttpGet("changepassword")]
        public IActionResult ChangePassword()
        {
            var model = new ChangePasswordModel();

            return View(model);
        }

        [Authorize]
        [HttpPost("changepassword")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var response = await authApiService.PostChangePasswordAsync(model);

            if (response.Success)
            {
                SetTokenCookies(response.Data);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                UpdateModelStateErrors(ModelState, response.Errors, response.ErrorMessage);

                return View(model);
            }
        }

        [Authorize]
        [HttpPost("logout")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> LogOut()
        {
            var response = await authApiService.GetAuthLogoutAsync();

            if (response.Success)
            {
                HttpContext.Response.Cookies.Delete(Services.WebApi.Constants.JwtCookiesKey);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(Constants.ErrorView, (response.ErrorMessage, controller: string.Empty, action: string.Empty));
            }
        }

        private void SetTokenCookies(string jwtToken)
        {
            HttpContext.Response.Cookies.Append(Services.WebApi.Constants.JwtCookiesKey, jwtToken,
                new CookieOptions
                {
                    HttpOnly = true,
                    SameSite = SameSiteMode.Strict,
                });
        }

        protected static void UpdateModelStateErrors(ModelStateDictionary modelState, FieldErrorModel[] errors, string errorMessage = null)
        {
            if (errorMessage != null)
            {
                modelState.AddModelError(string.Empty, errorMessage);
            }

            if (errors == null || errors.Length == 0)
            {
                return;
            }

            foreach (var error in errors)
            {
                if (!string.IsNullOrEmpty(error.Field))
                {
                    modelState.AddModelError(error.Field, error.Message);
                }
            }
        }
    }
}