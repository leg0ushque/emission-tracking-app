using AutoMapper;
using EmisTracking.Localization;
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
            itemDto.Id = Guid.NewGuid().ToString();

            var result = await _entityService.AddAsync(itemDto);

            return Ok(new ApiResponseModel<string>
            {
                Success = true,
                StatusCode = System.Net.HttpStatusCode.Created,
                Data = result
            });
        }

        [Authorize]
        [HttpGet("")]
        [BusinessLogicExceptionFilter]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAll([FromQuery] bool loadDependencies = false)
        {
            var items = await _entityService.GetAllAsync(loadDependencies: loadDependencies);
            var itemModelsList = _mapper.Map<List<TEntityModel>>(items);

            return Ok(new ApiResponseModel<List<TEntityModel>>
            {
                Success = true,
                StatusCode = System.Net.HttpStatusCode.OK,
                Data = itemModelsList
            });
        }

        [Authorize]
        [HttpGet("{id}")]
        [BusinessLogicExceptionFilter]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetById([FromRoute] string id, [FromQuery] bool loadDependencies = false)
        {
            var item = await _entityService.GetByIdAsync(id, loadDependencies);

            if(item == null)
            {
                return NotFound(new ApiResponseModel<object>
                {
                    Success = false,
                    StatusCode = System.Net.HttpStatusCode.NotFound,
                    ErrorMessage = string.Format(LangResources.ItemNotFoundMessageTemplate, nameof(TEntity), item.Id)
                });
            }

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
            var updateSuccessful = await _entityService.UpdateAsync(itemDto);

            return updateSuccessful ?
                Ok(new ApiResponseModel<object> { Success = true })
                : NotFound(new ApiResponseModel<object>
                {
                    Success = false,
                    StatusCode = System.Net.HttpStatusCode.NotFound,
                    ErrorMessage = string.Format(LangResources.ItemNotFoundMessageTemplate, nameof(TEntity), item.Id)
                });
        }

        [Authorize]
        [HttpDelete("{id}")]
        [BusinessLogicExceptionFilter]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            var deleteSuccessful = await _entityService.DeleteAsync(id);

            return deleteSuccessful ?
                Ok(new ApiResponseModel<object> { Success = true })
                : NotFound(new ApiResponseModel<object>
                {
                    Success = false,
                    StatusCode = System.Net.HttpStatusCode.NotFound,
                    ErrorMessage = string.Format(LangResources.ItemNotFoundMessageTemplate, nameof(TEntity), id)
                });
        }
    }
}
