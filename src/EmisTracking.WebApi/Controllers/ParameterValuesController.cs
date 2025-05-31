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
    public class ParameterValuesController : EntityController<ParameterValueViewModel, ParameterValue>
    {
        public ParameterValuesController(IEntityService<ParameterValue> service, IMapper mapper)
        {
            _entityService = service;
            _mapper = mapper;
        }

        protected override string EntityName => LangResources.Entities.ParameterValue;

        [Authorize]
        [HttpGet("byParameter/{id}")]
        [BusinessLogicExceptionFilter]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAllByParameterId(
            [FromRoute] string id,
            [FromQuery] bool loadDependencies = false)
        {
            var items = await _entityService.GetAllAsync(g => g.MethodologyParameterId == id,
                loadDependencies: loadDependencies);
            var itemModelsList = _mapper.Map<List<ParameterValueViewModel>>(items);

            return Ok(new ApiResponseModel<List<ParameterValueViewModel>>
            {
                Success = true,
                StatusCode = System.Net.HttpStatusCode.OK,
                Data = itemModelsList
            });
        }
    }
}
