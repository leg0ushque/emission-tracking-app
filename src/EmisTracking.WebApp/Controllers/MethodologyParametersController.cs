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
    [Route("[controller]")]
    public class MethodologyParametersController : BaseDropdownViewController<MethodologyParameterViewModel>
    {
        private readonly IBaseApiService<MethodologyViewModel> _methodologyApiService;

        public MethodologyParametersController(
            IBaseApiService<MethodologyParameterViewModel> apiService,
            IBaseApiService<MethodologyViewModel> methodologyApiService)
        {
            _apiService = apiService;
            _methodologyApiService = methodologyApiService;
        }

        public override async Task LoadDropdownsValuesAsync(MethodologyParameterViewModel model)
        {
            var methodologiesResponse = await _methodologyApiService.GetAllAsync();

            if (methodologiesResponse.Success)
            {
                model.Methodologies = methodologiesResponse.Data
                    .Select(cg => new DropdownItemModel { Value = cg.Id, Name = cg.Name })
                    .ToList();
            }
        }

        protected override string CreationTitle => LangResources.Titles.MethodologyParametersCreate;
        protected override string UpdateTitle => LangResources.Titles.MethodologyParametersUpdate;

        [Authorize]
        [LoadLayoutDataFilter]
        [HttpGet("createForMethodology/{id}")]
        public async Task<IActionResult> CreateForMethodology([FromRoute] string id, [FromQuery] string formulaName)
        {
            ViewData[AspAction] = nameof(Create);
            ViewData[Title] = CreationTitle;

            var model = new MethodologyParameterViewModel();
            await LoadDropdownsValuesAsync(model);

            model.MethodologyId = model.Methodologies.Any(s => s.Value == id) ? id : null;

            model.FormulaName = formulaName;

            return View(Constants.FormView, model);
        }
    }
}
