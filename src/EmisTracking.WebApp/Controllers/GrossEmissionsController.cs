using EmisTracking.Localization;
using EmisTracking.Services.Entities;
using EmisTracking.Services.WebApi.Services;
using EmisTracking.WebApi.Models.Models;
using EmisTracking.WebApi.Models.ViewModels;
using EmisTracking.WebApp.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
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
            var calculationResponse = await _grossEmissionService.СheckCalculation(model);

            if(calculationResponse.Success)
            {
                return View(nameof(Calculate), calculationResponse.Data); // Остаёмся на той же странице, обновляя данные
            }
            else
            {
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
                    ErrorMessage = calculationResponse.ErrorMessage
                };

                return View(nameof(Calculate), pageModel);
            }
        }

        [HttpPost("finalize")]
        public async Task<IActionResult> FinalizeHandler([FromForm] CalculationFormViewModel model)
        {
            var finalCalculationResponse = await _grossEmissionService.Calculate(model);

            if (finalCalculationResponse.Success)
            {
                return View("CalculationResult", finalCalculationResponse.Data);
            }
            else
            {
                return View(Constants.ErrorView,
                    (errorMessage: finalCalculationResponse.ErrorMessage, controller: "GrossEmissions", action: nameof(Calculate)));
            }
        }
    }
}
