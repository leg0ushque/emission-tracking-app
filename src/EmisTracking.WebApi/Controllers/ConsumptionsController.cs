using AutoMapper;
using EmisTracking.Services.Entities;
using EmisTracking.Services.Interfaces;
using EmisTracking.WebApi.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EmisTracking.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConsumptionsController : EntityController<ConsumptionViewModel, Consumption>
    {
        public ConsumptionsController(IEntityService<Consumption> service, IMapper mapper)
        {
            _entityService = service;
            _mapper = mapper;
        }
    }
}
