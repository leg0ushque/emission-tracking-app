using EmisTracking.WebApp.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmisTracking.WebApp.Controllers
{
    public class StatsController : Controller
    {
        [HttpGet("")]
        [Authorize]
        [LoadLayoutDataFilter]
        public IActionResult Index()
        {
            return View();
        }
    }
}
