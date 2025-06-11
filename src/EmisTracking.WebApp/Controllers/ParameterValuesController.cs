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
        [HttpGet("createForParameter/{id}")]
        public async Task<IActionResult> CreateForEmissionSource([FromRoute] string id)
        {
            ViewData[AspAction] = nameof(Create);
            ViewData[Title] = CreationTitle;

            var model = new ParameterValueViewModel();
            await LoadDropdownsValuesAsync(model);

            model.MethodologyParameterId = model.MethodologyParameters.Any(s => s.Value == id) ? id : null;

            return View(Constants.FormView, model);
        }
    }

}
