using EmisTracking.Localization;
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
    [Authorize(Roles = $"{Services.Constants.OperatorRole},{Services.Constants.AdminRole}")]
    public class OperatingTimesController : BaseDropdownViewController<OperatingTimeViewModel>
    {
        private readonly IBaseApiService<EmissionSourceViewModel> _emissionSourceService;
        private readonly IOperatingTimeApiService _operatingTimeService;

        public OperatingTimesController(
            IOperatingTimeApiService operatingTimeService,
            IBaseApiService<EmissionSourceViewModel> emissionSourceService)
        {
            _apiService = operatingTimeService;
            _emissionSourceService = emissionSourceService;
            _operatingTimeService = operatingTimeService;
        }

        protected override string CreationTitle => LangResources.Titles.OperatingTimesCreate;
        protected override string UpdateTitle => LangResources.Titles.OperatingTimesUpdate;

        public override async Task LoadDropdownsValuesAsync(OperatingTimeViewModel model)
        {
            var emissionSourcesResponse = await _emissionSourceService.GetAllAsync();

            if (emissionSourcesResponse.Success)
            {
                model.EmissionSources = emissionSourcesResponse.Data
                    .Select(e => new DropdownItemModel { Value = e.Id, Name = e.Name })
                    .ToList();
            }
        }

        [Authorize]
        [LoadLayoutDataFilter]
        [HttpGet("bySource/{id}")]
        public async Task<IActionResult> GetBySource([FromRoute] string id)
        {
            var response = await _operatingTimeService.GetByEmissionSourceIdAsync(id, loadDependencies: false);

            if (response.Success)
            {
                ViewData["SourceId"] = id;

                var sourceResponse = await _emissionSourceService.GetByIdAsync(id);
                ViewData["SourceInfo"] = sourceResponse.Success ? sourceResponse.Data.Name : string.Empty;

                return View("Index", response.Data);
            }
            else
            {
                return View(Constants.ErrorView, (errorMessage: response.ErrorMessage, controller: string.Empty, action: nameof(Index)));
            }
        }

        [Authorize]
        [LoadLayoutDataFilter]
        [HttpGet("createForEmissionSource/{id}")]
        public async Task<IActionResult> CreateForEmissionSource([FromRoute] string id,
            [FromQuery] int? month,
            [FromQuery] int? year)
        {
            ViewData[AspAction] = nameof(Create);
            ViewData[Title] = CreationTitle;

            var model = new OperatingTimeViewModel();
            await LoadDropdownsValuesAsync(model);

            model.EmissionSourceId = model.EmissionSources.Any(s => s.Value == id) ? id : null;

            var currentDate = DateTime.Now;

            model.Month = month.HasValue ? month.Value : currentDate.Month;
            model.Year = year.HasValue ? year.Value : currentDate.Year;

            return View(Constants.FormView, model);
        }
    }
}
