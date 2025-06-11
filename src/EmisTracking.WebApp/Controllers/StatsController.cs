using EmisTracking.WebApp.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmisTracking.WebApp.Controllers
{
    [LoadLayoutDataFilter]
    [Route("[controller]")]
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
