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
    public class SubdivisionsController : EntityController<SubdivisionViewModel, Subdivision>
    {
        public SubdivisionsController(IEntityService<Subdivision> service, IMapper mapper)
        {
            _entityService = service;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet("ofArea")]
        [BusinessLogicExceptionFilter]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAllByAreaId([FromQuery] string areaId)
        {
            List<Subdivision> list;

            if(string.IsNullOrEmpty(areaId))
            {
                ModelState.AddModelError(string.Empty, LangResources.EmptyIdText);

                return CreateBadRequestResponse(ModelState);
            }

            var items = await _entityService.GetAllAsync(s => s.AreaId == areaId);

            var itemModelsList = _mapper.Map<List<Subdivision>>(items);

            return Ok(new ApiResponseModel<List<Subdivision>> { Success = true, Data = itemModelsList });
        }

        //[Authorize]
        //[HttpDelete("{id}")]
        //[BusinessLogicExceptionFilter]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //public override async Task<IActionResult> Delete([FromRoute] string id)
        //{
        //    var

        //    await _entityService.DeleteAsync(id);

        //    return Ok(new ApiResponseModel<object> { Success = true });
        //}
    }
}
