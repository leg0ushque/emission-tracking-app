using Microsoft.AspNetCore.Mvc;
using EmisTracking.WebApp.Filters;

namespace EmisTracking.WebApp.Controllers
{
    [LoadLayoutDataFilter]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
