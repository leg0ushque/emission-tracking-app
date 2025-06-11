using AutoMapper;
using EmisTracking.Localization;
using EmisTracking.Services.Entities;
using EmisTracking.Services.Interfaces;
using EmisTracking.Services.Services;
using EmisTracking.WebApi.Filters;
using EmisTracking.WebApi.Models.Models;
using EmisTracking.WebApi.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EmisTracking.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GrossEmissionsController : EntityController<GrossEmissionViewModel, GrossEmission>
    {
        private readonly IGrossEmissionService _grossEmissionService;
        private readonly IEntityService<Methodology> _methodologyService;
        private readonly IEntityService<EmissionSource> _emissionSourceService;

        public GrossEmissionsController(IEntityService<GrossEmission> service, IMapper mapper,
            IGrossEmissionService grossEmissionService)
        {
            _entityService = service;
            _mapper = mapper;

            _grossEmissionService = grossEmissionService;
        }

        protected override string EntityName => LangResources.Entities.GrossEmission;

        [Authorize]
        [HttpPost("checkCalculation")]
        [BusinessLogicExceptionFilter]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public virtual async Task<IActionResult> CheckCalculation([FromBody] CalculationCheckResultViewModel item)
        {
            if (item is null)
            {
                return CreateBadRequestResponse(ModelState);
            }

            if (string.IsNullOrEmpty(item.MethodologyId))
            {
                ModelState.AddModelError(nameof(item.MethodologyId), LangResources.MustBeFilledMessage);

                return CreateBadRequestResponse(ModelState);
            }

            if (string.IsNullOrEmpty(item.EmissionSourceId))
            {
                ModelState.AddModelError(nameof(item.EmissionSourceId), LangResources.MustBeFilledMessage);

                return CreateBadRequestResponse(ModelState);
            }

            var foundMethodology = await _methodologyService.GetByIdAsync(item.MethodologyId);
            if (foundMethodology == null)
            {
                ModelState.AddModelError(nameof(item.MethodologyId),
                    string.Format(LangResources.ItemNotFoundMessageTemplate, LangResources.Entities.Methodology, item.MethodologyId));

                return CreateBadRequestResponse(ModelState);
            }

            var foundEmissionSource = await _emissionSourceService.GetByIdAsync(item.EmissionSourceId);
            if (foundEmissionSource == null)
            {
                ModelState.AddModelError(nameof(item.MethodologyId),
                    string.Format(LangResources.ItemNotFoundMessageTemplate, LangResources.Entities.Methodology, item.MethodologyId));

                return CreateBadRequestResponse(ModelState);
            }

            var result = await _grossEmissionService.CheckCalculationAsync(
                foundMethodology.Id,
                foundMethodology.ShortName,
                foundEmissionSource.Id,
                foundEmissionSource.Name,
                item.Month, item.Year);

            return Ok(new ApiResponseModel<CalculationCheckResultViewModel>
            {
                Data = _mapper.Map<CalculationCheckResultViewModel>(result),
                Success = true,
                StatusCode = System.Net.HttpStatusCode.OK
            });
        }

        // _mapper.Map<TEntity>(item);
        //    itemDto.Id = Guid.NewGuid().ToString();

        //    var result = await _entityService.AddAsync(itemDto);

        //    return Ok(new ApiResponseModel<string>
        //    {
        //        Success = true,
        //        StatusCode = System.Net.HttpStatusCode.Created,
        //        Data = result
        //    });
        //}


        //var missingParameterModel = new MissingGrossParameterViewModel
        //{
        //    MethodologyId = model.MethodologyId,
        //    MethodologyName = model.MethodologyName,
        //    EmissionSourceId = model.EmissionSourceId,
        //    EmissionSourceName = model.EmissionSourceName,
        //    ParameterId = calculationRespose.Data.MissingParameter.ParameterId,
        //    ParameterName = calculationRespose
        //};

    }
}
