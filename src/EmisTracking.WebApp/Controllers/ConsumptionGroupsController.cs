using EmisTracking.Localization;
using EmisTracking.Services.WebApi.Services;
using EmisTracking.WebApi.Models.Models;
using EmisTracking.WebApi.Models.ViewModels;
using EmisTracking.WebApp.Filters;
using EmisTracking.WebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmisTracking.WebApp.Controllers
{
    [LoadLayoutDataFilter]
    [Route("[controller]")]
    public class ConsumptionGroupsController : BaseDropdownViewController<ConsumptionGroupViewModel>
    {
        private readonly IBaseApiService<MethodologyViewModel> _methodologyService;
        private readonly ISpecificIndicatorApiService _specificIndicatorService;

        public ConsumptionGroupsController(
            IBaseApiService<ConsumptionGroupViewModel> consumptionGroupService,
            IBaseApiService<MethodologyViewModel> methodologyService,
            ISpecificIndicatorApiService specificIndicatorService)
        {
            _apiService = consumptionGroupService;
            _methodologyService = methodologyService;
            _specificIndicatorService = specificIndicatorService;
        }

        protected override string CreationTitle => LangResources.Titles.ConsumptionGroupsCreate;
        protected override string UpdateTitle => LangResources.Titles.ConsumptionGroupsUpdate;

        public override async Task LoadDropdownsValuesAsync(ConsumptionGroupViewModel model)
        {
            var methodologiesResponse = await _methodologyService.GetAllAsync();
            if (methodologiesResponse.Success)
            {
                model.Methodologies = methodologiesResponse.Data
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
                var specificIndicators = await _specificIndicatorService.GetByConsumptionGroupIdAsync(response.Data.Id, true);

                var model = new ModelWithDependencies<ConsumptionGroupViewModel, SpecificIndicatorViewModel>
                {
                    MainItem = response.Data,
                    Dependencies = specificIndicators.Success ? specificIndicators.Data : []
                };

                return View(model);
            }
            else
            {
                return View(Constants.ErrorView, (errorMessage: response.ErrorMessage, controller: string.Empty, action: nameof(Index)));
            }
        }

        [Authorize]
        [HttpGet("createForMethodology/{id}")]
        public async Task<IActionResult> CreateForMethodology([FromRoute] string id)
        {
            ViewData[AspAction] = nameof(Create);
            ViewData[Title] = CreationTitle;

            var model = new ConsumptionGroupViewModel();
            await LoadDropdownsValuesAsync(model);

            model.MethodologyId = model.Methodologies.Any(s => s.Value == id) ? id : null;

            return View(Constants.FormView, model);
        }
    }
}
