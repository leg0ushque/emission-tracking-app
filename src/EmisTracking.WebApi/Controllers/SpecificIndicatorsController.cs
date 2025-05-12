using AutoMapper;
using EmisTracking.Services.Entities;
using EmisTracking.Services.Interfaces;
using EmisTracking.WebApi.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EmisTracking.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SpecificIndicatorsController : EntityController<SpecificIndicatorViewModel, SpecificIndicator>
    {
        public SpecificIndicatorsController(IEntityService<SpecificIndicator> service, IMapper mapper)
        {
            _entityService = service;
            _mapper = mapper;
        }
    }
}
