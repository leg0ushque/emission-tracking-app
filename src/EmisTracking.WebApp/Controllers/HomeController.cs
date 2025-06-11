using Microsoft.AspNetCore.Mvc;
using EmisTracking.WebApp.Filters;
using EmisTracking.Services.WebApi.Services;
using System.Threading.Tasks;

namespace EmisTracking.WebApp.Controllers
{
    [LoadLayoutDataFilter]
    public class HomeController : Controller
    {
        private readonly IAuthApiService _authApiService;

        public HomeController(IAuthApiService authApiService)
        {
            _authApiService = authApiService;
        }

        public async Task<IActionResult> Index()
        {
            var pingResult = await _authApiService.GetPing();

            if(!pingResult.Success)
            {
                return View("ApiIsNotReachable");
            }

            return View();
        }
    }
}
