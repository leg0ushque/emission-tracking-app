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
    public class ConsumptionsController : BaseDropdownViewController<ConsumptionViewModel>
    {
        private readonly IBaseApiService<ConsumptionGroupViewModel> _consumptionGroupService;

        public ConsumptionsController(IBaseApiService<ConsumptionViewModel> consumptionService, IBaseApiService<ConsumptionGroupViewModel> consumptionGroupService)
        {
            _apiService = consumptionService;
            _consumptionGroupService = consumptionGroupService;
        }

        protected override string CreationTitle => LangResources.Titles.ConsumptionsCreate;
        protected override string UpdateTitle => LangResources.Titles.ConsumptionsUpdate;

        public override async Task LoadDropdownsValuesAsync(ConsumptionViewModel model)
        {
            var consumptionGroupsResponse = await _consumptionGroupService.GetAllAsync();

            if (consumptionGroupsResponse.Success)
            {
                model.ConsumptionGroups = consumptionGroupsResponse.Data
                    .Select(cg => new DropdownItemModel { Value = cg.Id, Name = cg.Name })
                    .ToList();
            }
        }

        [Authorize]
        [LoadLayoutDataFilter]
        [HttpGet("createForConsumptionGroup/{id}")]
        public async Task<IActionResult> CreateForConsumptionGroup([FromRoute] string id)
        {
            ViewData[AspAction] = nameof(Create);
            ViewData[Title] = CreationTitle;

            var model = new ConsumptionViewModel();
            await LoadDropdownsValuesAsync(model);

            model.ConsumptionGroupId = model.ConsumptionGroups.Any(s => s.Value == id) ? id : null;

            return View(Constants.FormView, model);
        }
    }
}
