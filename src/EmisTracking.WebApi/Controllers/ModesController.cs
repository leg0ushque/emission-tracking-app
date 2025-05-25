using AutoMapper;
using EmisTracking.Localization;
using EmisTracking.Services.Entities;
using EmisTracking.Services.Interfaces;
using EmisTracking.WebApi.Filters;
using EmisTracking.WebApi.Models.Models;
using EmisTracking.WebApi.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmisTracking.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ModesController : EntityController<ModeViewModel, Mode>
    {
        private readonly IEntityService<Methodology> _methodologiesService;

        public ModesController(IEntityService<Mode> service,
            IEntityService<Methodology> methodologiesService,
            IMapper mapper)
        {
            _entityService = service;
            _mapper = mapper;
            _methodologiesService = methodologiesService;
        }

        protected override string EntityName => LangResources.Entities.Mode;

        [Authorize]
        [HttpGet("withMethodologies")]
        [BusinessLogicExceptionFilter]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAllWithMethodologies()
        {
            var methodologies = await _methodologiesService.GetAllAsync(loadDependencies: true);
            var methodologiesModes = methodologies.Select(x => x.Mode).Where(x => x != null).ToList();

            var itemModelsList = _mapper.Map<List<ModeViewModel>>(methodologiesModes);

            return Ok(new ApiResponseModel<List<ModeViewModel>>
            {
                Success = true,
                StatusCode = System.Net.HttpStatusCode.OK,
                Data = itemModelsList
            });
        }
    }
}
