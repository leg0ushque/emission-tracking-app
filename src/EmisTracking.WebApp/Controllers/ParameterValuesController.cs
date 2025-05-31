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
        private readonly IBaseApiService<MethodologyParameterViewModel> _methodologyParameterService;

        public ParameterValuesController(
            IBaseApiService<ParameterValueViewModel> parameterValueService,
            IBaseApiService<MethodologyParameterViewModel> methodologyParameterService,
            IBaseApiService<GrossEmissionViewModel> grossEmissionService)
        {
            _apiService = parameterValueService;
            _methodologyParameterService = methodologyParameterService;
        }

        protected override string CreationTitle => LangResources.Titles.ParameterValuesCreate;
        protected override string UpdateTitle => LangResources.Titles.ParameterValuesUpdate;

        public override async Task LoadDropdownsValuesAsync(ParameterValueViewModel model)
        {
            var methodologyParametersResponse = await _methodologyParameterService.GetAllAsync();

            if (methodologyParametersResponse.Success)
            {
                model.MethodologyParameters = methodologyParametersResponse.Data
                    .Select(mp => new DropdownItemModel { Value = mp.Id, Name = mp.Name })
                    .ToList();
            }
        }

        [Authorize]
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
