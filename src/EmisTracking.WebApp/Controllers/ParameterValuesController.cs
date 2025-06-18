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
    public class ParameterValuesController : BaseDropdownViewController<ParameterValueViewModel>
    {
        private readonly ISourceSubstanceApiService _sourceSubstanceApiService;
        private readonly IBaseApiService<MethodologyViewModel> _methodologyService;
        private readonly IBaseApiService<EmissionSourceViewModel> _emissionSourceService;
        private readonly IBaseApiService<MethodologyParameterViewModel> _methodologyParameterService;

        public ParameterValuesController(
            ISourceSubstanceApiService sourceSubstanceApiService,
            IBaseApiService<MethodologyViewModel> methodologyService,
            IBaseApiService<EmissionSourceViewModel> emissionSourceService,
            IBaseApiService<ParameterValueViewModel> parameterValueService,
            IBaseApiService<MethodologyParameterViewModel> methodologyParameterService)
        {
            _apiService = parameterValueService;
            _sourceSubstanceApiService = sourceSubstanceApiService;
            _methodologyService = methodologyService;
            _emissionSourceService = emissionSourceService;
            _methodologyParameterService = methodologyParameterService;
        }

        protected override string CreationTitle => LangResources.Titles.ParameterValuesCreate;
        protected override string UpdateTitle => LangResources.Titles.ParameterValuesUpdate;

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
                var methodologyResponse = await _methodologyService.GetByIdAsync(response.Data.MethodologyParameter.MethodologyId);

                if (methodologyResponse.Success)
                    response.Data.MethodologyParameter.Methodology = methodologyResponse.Data;

                return View(response.Data);
            }
            else
            {
                return View(Constants.ErrorView, (errorMessage: response.ErrorMessage, controller: string.Empty, action: nameof(Index)));
            }
        }

        public override async Task LoadDropdownsValuesAsync(ParameterValueViewModel model)
        {
            var methodologyParametersResponse = await _methodologyParameterService.GetAllAsync();

            var sourceSubstancesResponse = await _methodologyParameterService.GetAllAsync();

            model.MethodologyParameters = methodologyParametersResponse.Success ? methodologyParametersResponse.Data
                .Select(mp => new DropdownItemModel { Value = mp.Id, Name = mp.Name })
                .ToList() : [];

            model.SourceSubstances = sourceSubstancesResponse.Success ? sourceSubstancesResponse.Data
                .Select(mp => new DropdownItemModel { Value = mp.Id, Name = mp.Name })
                .ToList() : [];
        }

        [Authorize]
        [LoadLayoutDataFilter]
        [HttpGet("createForParameter/{methodologyParameterId}")]
        public async Task<IActionResult> CreateForParameter([FromRoute] string methodologyParameterId,
            [FromQuery] int? month,
            [FromQuery] int? year)
        {
            ViewData[AspAction] = nameof(Create);
            ViewData[Title] = CreationTitle;

            var model = new ParameterValueViewModel();
                await LoadDropdownsValuesAsync(model);

            model.MethodologyParameterId = model.MethodologyParameters.Any(s =>
                s.Value == methodologyParameterId) ? methodologyParameterId : null;

            if(month.HasValue)
                model.Month = month.Value;

            if (year.HasValue)
                model.Month = year.Value;

            return View(Constants.FormView, model);
        }
    }

}
