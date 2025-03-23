using Microsoft.AspNetCore.Mvc;
using EmisTracking.WebApp.Filters;

namespace EmisTracking.WebApp.Controllers
{
    public class HomeController : Controller
    {
        [LoadLayoutDataFilter]
        public IActionResult Index()
        {
            return View();
        }
    }
}
