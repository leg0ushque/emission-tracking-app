using AutoMapper;
using EmisTracking.Services.Interfaces;
using EmisTracking.WebApi.Filters;
using EmisTracking.WebApi.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ServicesConstants = EmisTracking.Services.Constants;

namespace EmisTracking.WebApi.Controllers
{
    public abstract class EntityController<TEntityModel, TEntity> : ErrorHandlingController
        where TEntityModel : class, IViewModel
        where TEntity : class, IEntity
    {
        protected IEntityService<TEntity> _entityService;
        protected IMapper _mapper;

        [Authorize]
        [HttpPost("")]
        [BusinessLogicExceptionFilter]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public virtual async Task<IActionResult> Create(TEntityModel item)
        {
            if (item is null)
            {
                return CreateBadRequestResponse(ModelState);
            }

            var itemDto = _mapper.Map<TEntity>(item);
            var result = await _entityService.AddAsync(itemDto);

            return Ok(result);
        }

        [Authorize]
        [HttpGet("")]
        [BusinessLogicExceptionFilter]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAllAtPage(
            [FromQuery] int? pageNumber = ServicesConstants.MinPageNumber,
            [FromQuery] int? pageSize = ServicesConstants.DefaultPageSize)
        {
            var items = await _entityService.GetAllAtPageAsync(pageNumber, pageSize);

            return Ok(items);
        }

        [Authorize]
        [HttpGet("{id}")]
        [BusinessLogicExceptionFilter]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetById([FromRoute] string id)
        {
            var item = await _entityService.GetByIdAsync(id);

            return Ok(item);
        }

        [Authorize]
        [HttpPut("")]
        [BusinessLogicExceptionFilter]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Update([FromRoute] TEntityModel item)
        {
            if (item is null)
            {
                return CreateBadRequestResponse(ModelState);
            }

            var itemDto = _mapper.Map<TEntity>(item);
            await _entityService.UpdateAsync(itemDto);

            return Ok();
        }

        [Authorize]
        [HttpDelete("{id}")]
        [BusinessLogicExceptionFilter]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            await _entityService.DeleteAsync(id);

            return Ok();
        }
    }
}
