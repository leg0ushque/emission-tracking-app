using EmisTracking.Localization;
using EmisTracking.Services.WebApi.Helpers;
using EmisTracking.Services.WebApi.Services;
using EmisTracking.WebApi.Models.Models;
using EmisTracking.WebApp.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmisTracking.WebApp.Controllers
{
    [Route("[controller]")]
    public class AuthController(IAuthApiService authApiService) : Controller
    {
        private readonly IAuthApiService authApiService = authApiService;

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
        public async Task<IActionResult> PostLogin([FromForm] LoginModel model)
        {
            var response = await authApiService.PostSignInAsync(model);

            SetTokenCookies(response);

            return RedirectToAction("Index", "Home");
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
        public async Task<IActionResult> ChangePassword([FromForm] ChangePasswordModel model)
        {
            var response = await authApiService.PostChangePasswordAsync(model);

            if(response.IsSuccessStatusCode)
            {
                var token = await response.Content.ReadAsStringAsync();
                SetTokenCookies(token);

                return RedirectToAction("Index", "Home");
            }

            //var errors = await response.DeserializeContentAsync<AuthErrorModel[]>();
            //var error = errors.FirstOrDefault();

            ModelState.AddModelError(string.Empty, LangResources.ChangePasswordError);

            return View(model);
        }

        [Authorize]
        [HttpPost("logout")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> LogOut()
        {
            await authApiService.GetAuthLogoutAsync();

            HttpContext.Response.Cookies.Delete(Services.WebApi.Constants.JwtCookiesKey);

            return RedirectToAction("Index", "Home");
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
    }
}
