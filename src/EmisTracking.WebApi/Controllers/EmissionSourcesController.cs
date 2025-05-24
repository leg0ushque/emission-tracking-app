using AutoMapper;
using EmisTracking.Localization;
using EmisTracking.Services.Entities;
using EmisTracking.Services.Interfaces;
using EmisTracking.WebApi.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EmisTracking.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmissionSourcesController : EntityController<EmissionSourceViewModel, EmissionSource>
    {
        public EmissionSourcesController(IEntityService<EmissionSource> service, IMapper mapper)
        {
            _entityService = service;
            _mapper = mapper;
        }

        protected override string EntityName => LangResources.Entities.EmissionSource;
    }
}
