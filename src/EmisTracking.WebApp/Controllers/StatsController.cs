using EmisTracking.WebApp.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmisTracking.WebApp.Controllers
{
    [LoadLayoutDataFilter]
    [Route("[controller]")]
    [Authorize(Roles = $"{Services.Constants.DirectorRole},{Services.Constants.AccountantRole},{Services.Constants.AdminRole}}")]
    public class StatsController : Controller
    {
        [Authorize]
        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
