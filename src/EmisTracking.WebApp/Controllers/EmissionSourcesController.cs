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
    public class EmissionSourcesController : BaseDropdownViewController<EmissionSourceViewModel>
    {
        private readonly IBaseApiService<SubdivisionViewModel> _subdivisionService;
        private readonly IBaseApiService<MethodologyViewModel> _methodologyService;
        private readonly IBaseApiService<PollutantViewModel> _pollutantService;

        public EmissionSourcesController(
            IBaseApiService<EmissionSourceViewModel> emissionSourceService,
            IBaseApiService<SubdivisionViewModel> subdivisionService,
            IBaseApiService<MethodologyViewModel> methodologyService,
            IBaseApiService<PollutantViewModel> pollutantService)
        {
            _apiService = emissionSourceService;
            _apiService = emissionSourceService;
            _subdivisionService = subdivisionService;
            _methodologyService = methodologyService;
            _pollutantService = pollutantService;
        }

        protected override string CreationTitle => LangResources.Titles.EmissionSourcesCreate;
        protected override string UpdateTitle => LangResources.Titles.EmissionSourcesUpdate;

        [Authorize(Roles = Services.Constants.AdminRole)]
        [HttpGet("createForSubdivision/{id}")]
        public async Task<IActionResult> CreateForSubdivision([FromRoute] string id)
        {
            ViewData[AspAction] = nameof(Create);
            ViewData[Title] = CreationTitle;

            var model = new EmissionSourceViewModel();
            await LoadDropdownsValuesAsync(model);
            model.SubdivisionId = model.Subdivisions.Any(s => s.Value == id) ? id : null;

            return View(Constants.FormView, model);
        }

        [Authorize]
        [LoadLayoutDataFilter]
        [HttpGet("{id}")]
        public override async Task<IActionResult> Item([FromRoute] string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return View(Constants.ErrorView, (LangResources.EmptyIdText, controller: string.Empty, action: nameof(Index)));
            }

            var response = await _apiService.GetByIdAsync(id, loadDependencies: true);

            if (response.Success)
            {
                var pollutants = _pollutantService.GetAllAsync();

                return View(response.Data);
            }
            else
            {
                return View(Constants.ErrorView, (errorMessage: response.ErrorMessage, controller: string.Empty, action: nameof(Index)));
            }
        }

        public override async Task LoadDropdownsValuesAsync(EmissionSourceViewModel model)
        {
            var subdivisionsResponse = await _subdivisionService.GetAllAsync();
            var methodologiesResponse = await _methodologyService.GetAllAsync();

            if (subdivisionsResponse.Success && methodologiesResponse.Success)
            {
                model.Subdivisions = subdivisionsResponse.Data
                    .Select(s => new DropdownItemModel { Value = s.Id, Name = s.Name })
                    .ToList();

                model.Methodologies = methodologiesResponse.Data
                    .Select(m => new DropdownItemModel { Value = m.Id, Name = m.Name })
                    .ToList();
            }
        }
    }
}
