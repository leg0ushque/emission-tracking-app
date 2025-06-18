using AutoMapper;
using EmisTracking.Localization;
using EmisTracking.Services.Entities;
using EmisTracking.Services.Interfaces;
using EmisTracking.Services.Models;
using EmisTracking.Services.Services;
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
    public class GrossEmissionsController : EntityController<GrossEmissionViewModel, GrossEmission>
    {
        private readonly IGrossEmissionService _grossEmissionService;
        private readonly IEntityService<Methodology> _methodologyService;
        private readonly IEntityService<EmissionSource> _emissionSourceService;

        public GrossEmissionsController(IEntityService<GrossEmission> service,
            IGrossEmissionService grossEmissionService,
            IEntityService<Methodology> methodologyService,
            IEntityService<EmissionSource> emissionSourceService,
            IMapper mapper)
        {
            _entityService = service;
            _mapper = mapper;

            _grossEmissionService = grossEmissionService;
            _methodologyService = methodologyService;
            _emissionSourceService = emissionSourceService;
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

            return Ok(new ApiResponseModel<CalculationCheckResult>
            {
                Data = result,
                Success = true,
                StatusCode = System.Net.HttpStatusCode.OK
            });
        }


        [Authorize]
        [HttpPost("calculate")]
        [BusinessLogicExceptionFilter]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public virtual async Task<IActionResult> Calculate([FromBody] CalculationCheckResultViewModel item)
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

            var calculationResult = await _grossEmissionService.CalculateAsync(
                foundMethodology,
                foundEmissionSource.Id,
                foundEmissionSource.Name,
                item.Month,
                item.Year);

            return Ok(new ApiResponseModel<List<GrossEmissionViewModel>>
            {
                Data = _mapper.Map<List<GrossEmissionViewModel>>(calculationResult),
                Success = true,
                StatusCode = System.Net.HttpStatusCode.OK
            });
        }
    }
}
