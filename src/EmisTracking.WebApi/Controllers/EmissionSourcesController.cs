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
using System.Threading.Tasks;

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

        [Authorize]
        [HttpGet("bySubdivision/{subdivisionId}")]
        [BusinessLogicExceptionFilter]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAllBySubdivision([FromRoute] string subdivisionId, [FromQuery] bool loadDependencies = false)
        {
            var items = await _entityService.GetAllAsync(e => e.SubdivisionId == subdivisionId,
                loadDependencies: loadDependencies);

            var itemModelsList = _mapper.Map<List<EmissionSourceViewModel>>(items);

            return Ok(new ApiResponseModel<List<EmissionSourceViewModel>>
            {
                Success = true,
                StatusCode = System.Net.HttpStatusCode.OK,
                Data = itemModelsList
            });
        }
    }
}
