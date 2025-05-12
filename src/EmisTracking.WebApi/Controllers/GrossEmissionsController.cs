using AutoMapper;
using EmisTracking.Services.Entities;
using EmisTracking.Services.Interfaces;
using EmisTracking.WebApi.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EmisTracking.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GrossEmissionsController : EntityController<GrossEmissionViewModel, GrossEmission>
    {
        public GrossEmissionsController(IEntityService<GrossEmission> service, IMapper mapper)
        {
            _entityService = service;
            _mapper = mapper;
        }
    }
}
