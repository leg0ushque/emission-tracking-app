using ClosedXML.Excel;
using EmisTracking.Localization;
using EmisTracking.Services.Entities;
using EmisTracking.Services.WebApi.Services;
using EmisTracking.WebApi.Models.Models;
using EmisTracking.WebApi.Models.ViewModels;
using EmisTracking.WebApp.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EmisTracking.WebApp.Controllers
{
    [LoadLayoutDataFilter]
    [Route("[controller]")]
    [Authorize(Roles = $"{Services.Constants.DirectorRole},{Services.Constants.AccountantRole},{Services.Constants.AdminRole}")]
    public class GrossEmissionsController : BaseDropdownViewController<GrossEmissionViewModel>
    {
        private readonly IGrossEmissionApiService _grossEmissionService;
        private readonly IBaseApiService<SourceSubstanceViewModel> _sourceSubstanceService;
        private readonly IBaseApiService<MethodologyViewModel> _methodologyService;
        private readonly IBaseApiService<EmissionSourceViewModel> _emissionSourceService;

        public GrossEmissionsController(
            IGrossEmissionApiService grossEmissionService,
            IBaseApiService<SourceSubstanceViewModel> sourceSubstanceService,
            IBaseApiService<MethodologyViewModel> methodologyService,
            IBaseApiService<EmissionSourceViewModel> emissionSourceService)
        {
            _apiService = grossEmissionService;
            _grossEmissionService = grossEmissionService;

            _sourceSubstanceService = sourceSubstanceService;
            _methodologyService = methodologyService;
            _emissionSourceService = emissionSourceService;
        }

        protected override string CreationTitle => LangResources.Titles.GrossEmissionsCreate;
        protected override string UpdateTitle => LangResources.Titles.GrossEmissionsUpdate;

        public override async Task LoadDropdownsValuesAsync(GrossEmissionViewModel model)
        {
            var sourceSubstancesResponse = await _sourceSubstanceService.GetAllAsync();
            var methodologiesResponse = await _methodologyService.GetAllAsync();

            if (sourceSubstancesResponse.Success && methodologiesResponse.Success)
            {
                model.SourceSubstances = sourceSubstancesResponse.Data
                    .Select(ss => new DropdownItemModel { Value = ss.Id, Name = ss.EmissionSourceId })
                    .ToList();

                model.Methodologies = methodologiesResponse.Data
                    .Select(m => new DropdownItemModel { Value = m.Id, Name = m.Name })
                    .ToList();
            }
        }

        [HttpGet("bySource/{id}")]
        public async Task<IActionResult> IndexBySource([FromRoute] string id)
        {
            var response = await _grossEmissionService.GetAllAsync(loadDependencies: true);

            if (response.Success)
            {
                var foundEmissionSource = await _emissionSourceService.GetByIdAsync(id);

                ViewData["EmissionSourceId"] = foundEmissionSource.Data?.Id;
                ViewData["EmissionSourceName"] = foundEmissionSource.Data?.Name ?? string.Empty;

                return View("Index", response.Data);
            }
            else
            {
                return View(Constants.ErrorView, (errorMessage: response.ErrorMessage, controller: string.Empty, action: nameof(Index)));
            }
        }

        [HttpGet("calculate/{emissionSourceId}")]
        public async Task<IActionResult> Calculate([FromRoute] string emissionSourceId)
        {
            var emissionSourceResponse = await _emissionSourceService.GetByIdAsync(emissionSourceId);

            if (emissionSourceResponse.Success)
            {
                var methodologiesResponse = await _methodologyService.GetAllAsync(loadDependencies: true);

                var currentDate = DateTime.Now;

                var model = new CalculationCheckResultViewModel
                {
                    EmissionSourceName = emissionSourceResponse.Data.Name,
                    EmissionSourceId = emissionSourceId,
                    Month = currentDate.Month,
                    Year = currentDate.Year,
                    MethodologyId = emissionSourceResponse.Data.MethodologyId,
                    MethodologyName = methodologiesResponse.Data.FirstOrDefault(x => x.Id == emissionSourceResponse.Data.MethodologyId)?.ShortName,
                    Methodologies = methodologiesResponse.Success ? methodologiesResponse.Data
                        .Select(x => new DropdownItemModel(x.Id, x.ShortName)).ToList() : [],
                };

                return View(model);
            }
            else
            {
                return View(Constants.ErrorView, (errorMessage: emissionSourceResponse.ErrorMessage, controller: string.Empty, action: nameof(Index)));
            }
        }

        [HttpPost("calculate")]
        public async Task<IActionResult> CalculateHandler([FromForm] CalculationFormViewModel model)
        {
            ApiResponseModel<CalculationCheckResultViewModel> calculationResponse = null;

            var existingGrossEmission = await _grossEmissionService.GetBySource(model.EmissionSourceId);

            if (existingGrossEmission.Success && !existingGrossEmission.Data.Any(i => i.Month == model.Month && i.Year == model.Year))
            {
                calculationResponse = await _grossEmissionService.СheckCalculation(model);

                if (calculationResponse.Success)
                {
                    return View(nameof(Calculate), calculationResponse.Data); // Остаёмся на той же странице, обновляя данные
                }
            }
            else
            {
                calculationResponse = new ApiResponseModel<CalculationCheckResultViewModel> { ErrorMessage = LangResources.AlreadyCalculated };
            }

            var methodologiesResponse = await _methodologyService.GetAllAsync(loadDependencies: true);

            var currentDate = DateTime.Now;

            var pageModel = new CalculationCheckResultViewModel
            {
                EmissionSourceName = model.EmissionSourceName,
                EmissionSourceId = model.EmissionSourceId,
                Month = currentDate.Month,
                Year = currentDate.Year,
                MethodologyId = model.MethodologyId,
                MethodologyName = methodologiesResponse.Success ?
                    methodologiesResponse.Data.FirstOrDefault(m => m.Id == model.MethodologyId).Name
                    : LangResources.NoValue,
                Methodologies = methodologiesResponse.Success ? methodologiesResponse.Data
                    .Select(x => new DropdownItemModel(x.Id, x.ShortName)).ToList() : [],
                ErrorMessage = calculationResponse?.ErrorMessage
            };

            return View(nameof(Calculate), pageModel);
        }

        [HttpPost("finalize")]
        public async Task<IActionResult> FinalizeHandler([FromForm] CalculationFormViewModel model)
        {
            var methodologiesResponse = await _methodologyService.GetAllAsync(loadDependencies: true);

            var currentDate = DateTime.Now;

            var finalCalculationResponse = await _grossEmissionService.Calculate(model);

            if (finalCalculationResponse.Success)
            {
                var calculationResultModel = new CalculationResultViewModel
                {
                    EmissionSourceName = model.EmissionSourceName,
                    EmissionSourceId = model.EmissionSourceId,
                    Month = currentDate.Month,
                    Year = currentDate.Year,
                    MethodologyId = model.MethodologyId,
                    MethodologyName = methodologiesResponse.Success ?
                        methodologiesResponse.Data.FirstOrDefault(m => m.Id == model.MethodologyId).Name
                        : LangResources.NoValue,
                    Methodologies = methodologiesResponse.Success ? methodologiesResponse.Data
                        .Select(x => new DropdownItemModel(x.Id, x.ShortName)).ToList() : [],
                    ErrorMessage = finalCalculationResponse.ErrorMessage,
                    GrossEmissions = finalCalculationResponse.Data
                };


                ViewData["EmissionSourceId"] = model.EmissionSourceId;
                ViewData["EmissionSourceName"] = model.EmissionSourceName ?? string.Empty;

                return RedirectToAction("Index", routeValues: new { id = model.EmissionSourceId });
            }
            else
            {
                var pageModel = new CalculationCheckResultViewModel
                {
                    EmissionSourceName = model.EmissionSourceName,
                    EmissionSourceId = model.EmissionSourceId,
                    Month = currentDate.Month,
                    Year = currentDate.Year,
                    MethodologyId = model.MethodologyId,
                    MethodologyName = methodologiesResponse.Success ?
                        methodologiesResponse.Data.FirstOrDefault(m => m.Id == model.MethodologyId).Name
                        : LangResources.NoValue,
                    Methodologies = methodologiesResponse.Success ? methodologiesResponse.Data
                        .Select(x => new DropdownItemModel(x.Id, x.ShortName)).ToList() : [],
                    ErrorMessage = finalCalculationResponse.ErrorMessage
                };

                return View(nameof(Calculate), pageModel);
            }
        }
    }
}
