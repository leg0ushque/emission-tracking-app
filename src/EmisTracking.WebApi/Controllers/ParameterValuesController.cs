using AutoMapper;
using EmisTracking.Services.Entities;
using EmisTracking.Services.Interfaces;
using EmisTracking.WebApi.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EmisTracking.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ParameterValuesController : EntityController<ParameterValueViewModel, ParameterValue>
    {
        public ParameterValuesController(IEntityService<ParameterValue> service, IMapper mapper)
        {
            _entityService = service;
            _mapper = mapper;
        }
    }
}
