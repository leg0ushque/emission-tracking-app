using EmisTracking.Services.Entities;
using EmisTracking.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmisTracking.WebApp.Controllers
{
    [Route("[controller]")]
    public class AreasController(IEntityService<Area> areasService, IEntityService<Department> departmentsService) : Controller
    {
        private readonly IEntityService<Area> _areasService = areasService;
        private readonly IEntityService<Department> _departmentService = departmentsService;
    }
}
