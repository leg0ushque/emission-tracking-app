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
    public class ConsumptionsController : EntityController<ConsumptionViewModel, Consumption>
    {
        public ConsumptionsController(IEntityService<Consumption> service, IMapper mapper)
        {
            _entityService = service;
            _mapper = mapper;
        }

        protected override string EntityName => LangResources.Entities.Consumption;

        [Authorize]
        [HttpGet("byConsumptionGroup/{id}")]
        [BusinessLogicExceptionFilter]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAllByConsumptionGroupId(
            [FromRoute] string id,
            [FromQuery] bool loadDependencies = false)
        {
            var items = await _entityService.GetAllAsync(g => g.ConsumptionGroupId == id,
                loadDependencies: loadDependencies);
            var itemModelsList = _mapper.Map<List<ConsumptionViewModel>>(items);

            return Ok(new ApiResponseModel<List<ConsumptionViewModel>>
            {
                Success = true,
                StatusCode = System.Net.HttpStatusCode.OK,
                Data = itemModelsList
            });
        }
    }
}
