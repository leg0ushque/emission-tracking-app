using AutoMapper;
using EmisTracking.Services.Entities;
using EmisTracking.Services.Interfaces;
using EmisTracking.WebApi.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EmisTracking.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConsumptionGroupsController : EntityController<ConsumptionGroupViewModel, ConsumptionGroup>
    {
        public ConsumptionGroupsController(IEntityService<ConsumptionGroup> service, IMapper mapper)
        {
            _entityService = service;
            _mapper = mapper;
        }
    }
}
