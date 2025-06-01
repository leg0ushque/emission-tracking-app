using EmisTracking.Localization;
using EmisTracking.Services.WebApi.Services;
using EmisTracking.WebApi.Models.Models;
using EmisTracking.WebApi.Models.ViewModels;
using EmisTracking.WebApp.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace EmisTracking.WebApp.Controllers
{
    [LoadLayoutDataFilter]
    [Route("[controller]")]
    public class SourceSubstancesController : BaseDropdownViewController<SourceSubstanceViewModel>
    {
        private readonly IBaseApiService<EmissionSourceViewModel> _emissionSourceService;
        private readonly IBaseApiService<PollutantViewModel> _pollutantService;

        public SourceSubstancesController(
            IBaseApiService<SourceSubstanceViewModel> sourceSubstanceService,
            IBaseApiService<EmissionSourceViewModel> emissionSourceService,
            IBaseApiService<PollutantViewModel> pollutantService)
        {
            _apiService = sourceSubstanceService;
            _emissionSourceService = emissionSourceService;
            _pollutantService = pollutantService;
        }

        protected override string CreationTitle => LangResources.Titles.SourceSubstancesCreate;
        protected override string UpdateTitle => LangResources.Titles.SourceSubstancesUpdate;

        public override async Task LoadDropdownsValuesAsync(SourceSubstanceViewModel model)
        {
            var emissionSourcesResponse = await _emissionSourceService.GetAllAsync();
            var pollutantsResponse = await _pollutantService.GetAllAsync();

            if (emissionSourcesResponse.Success && pollutantsResponse.Success)
            {
                model.EmissionSources = emissionSourcesResponse.Data
                    .Select(e => new DropdownItemModel { Value = e.Id, Name = e.Name })
                    .ToList();

                model.Pollutants = pollutantsResponse.Data
                    .Select(p => new DropdownItemModel { Value = p.Id, Name = p.Name })
                    .ToList();
            }
        }

        [Authorize]
        [LoadLayoutDataFilter]
        [HttpGet("createForEmissionSource/{emissionSourceId}")]
        public async Task<IActionResult> CreateForEmissionSource([FromRoute] string emissionSourceId)
        {
            var model = new SourceSubstanceViewModel();

            await LoadDropdownsValuesAsync(model);

            var selectedSource = model.EmissionSources.FirstOrDefault(e => e.Value == emissionSourceId);
            if (selectedSource != null)
            {
                model.EmissionSourceId = emissionSourceId;
            }

            ViewData[AspAction] = nameof(Create);
            ViewData[Title] = CreationTitle;

            return View(Constants.FormView, model);
        }
    }
}
