using AutoMapper;
using EmisTracking.Services.Interfaces;
using EmisTracking.WebApi.Filters;
using EmisTracking.WebApi.Models.Models;
using EmisTracking.WebApi.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        public virtual async Task<IActionResult> Create([FromBody] TEntityModel item)
        {
            if (item is null)
            {
                return CreateBadRequestResponse(ModelState);
            }

            var itemDto = _mapper.Map<TEntity>(item);
            itemDto.Id = Guid.NewGuid().ToString(); // FIXME

            var result = await _entityService.AddAsync(itemDto);

            return Ok(new ApiResponseModel<string> { Success = true, Data = result });
        }

        [Authorize]
        [HttpGet("")]
        [BusinessLogicExceptionFilter]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAll()
        {
            var items = await _entityService.GetAllAsync();
            var itemModelsList = _mapper.Map<List<TEntityModel>>(items);

            return Ok(new ApiResponseModel<List<TEntityModel>> { Success = true, Data = itemModelsList });
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
            var itemModel = _mapper.Map<TEntityModel>(item);

            return Ok(new ApiResponseModel<TEntityModel> { Success = true, Data = itemModel });
        }

        [Authorize]
        [HttpPut("")]
        [BusinessLogicExceptionFilter]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Update([FromBody] TEntityModel item)
        {
            if (item is null)
            {
                return CreateBadRequestResponse(ModelState);
            }

            var itemDto = _mapper.Map<TEntity>(item);
            await _entityService.UpdateAsync(itemDto);

            return Ok(new ApiResponseModel<object> { Success = true });
        }

        [Authorize]
        [HttpDelete("{id}")]
        [BusinessLogicExceptionFilter]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public virtual async Task<IActionResult> Delete([FromRoute] string id)
        {
            await _entityService.DeleteAsync(id);

            return Ok(new ApiResponseModel<object> { Success = true });
        }
    }
}
