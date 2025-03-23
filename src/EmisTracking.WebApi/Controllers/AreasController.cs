using AutoMapper;
using EmisTracking.Services.Entities;
using EmisTracking.Services.Interfaces;
using EmisTracking.WebApi.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EmisTracking.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AreasController : EntityController<AreaViewModel, Area>
    {
        public AreasController(IEntityService<Area> groupService, IMapper mapper)
        {
            _entityService = groupService;
            _mapper = mapper;
        }
    }
}
