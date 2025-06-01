using EmisTracking.Localization;
using EmisTracking.Services.WebApi.Services;
using EmisTracking.WebApi.Models.Models;
using EmisTracking.WebApi.Models.ViewModels;
using EmisTracking.WebApp.Filters;
using EmisTracking.WebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace EmisTracking.WebApp.Controllers
{
    [LoadLayoutDataFilter]
    [Route("[controller]")]
    public class MethodologiesController : BaseDropdownViewController<MethodologyViewModel>
    {
        private readonly IBaseApiService<ModeViewModel> _modeService;
        private readonly IMethodologyParameterApiService _methodologyParameterApiService;

        public MethodologiesController(
            IBaseApiService<MethodologyViewModel> methodologyService,
            IBaseApiService<ModeViewModel> modeService,
            IMethodologyParameterApiService methodologyParameterApiService)
        {
            _apiService = methodologyService;
            _modeService = modeService;
            _methodologyParameterApiService = methodologyParameterApiService;
        }

        protected override string CreationTitle => LangResources.Titles.MethodologiesCreate;
        protected override string UpdateTitle => LangResources.Titles.MethodologiesUpdate;

        public override async Task LoadDropdownsValuesAsync(MethodologyViewModel model)
        {
            var modesResponse = await _modeService.GetAllAsync();

            if (modesResponse.Success)
            {
                model.Modes = modesResponse.Data
                    .Select(m => new DropdownItemModel { Value = m.Id, Name = m.Name })
                    .ToList();
            }
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
                var parametersResponse = await _methodologyParameterApiService.GetByMethodologyIdAsync(response.Data.Id);

                var model = new ModelWithDependencies<MethodologyViewModel, MethodologyParameterViewModel>()
                {
                    MainItem = response.Data,
                    Dependencies = parametersResponse.Success ? parametersResponse.Data : []
                };

                var formulaParameters = model.MainItem.GetFormulaParameters();

                ViewData[Constants.MissingParameters] = formulaParameters
                    .Except(model.Dependencies.Select(x => x.FormulaName)).ToList();
                ViewData[Constants.ExtraParameters] = model.Dependencies
                    .Where(d => !formulaParameters.Contains(d.FormulaName))
                    .Select(x => (x.Id, x.Name)).ToList();

                return View(model);
            }
            else
            {
                return View(Constants.ErrorView, (errorMessage: response.ErrorMessage, controller: string.Empty, action: nameof(Index)));
            }
        }
    }
}
