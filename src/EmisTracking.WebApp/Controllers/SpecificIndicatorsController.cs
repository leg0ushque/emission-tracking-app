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
    [Authorize(Roles = $"{Services.Constants.EcologistRole},{Services.Constants.AdminRole}")]
    public class SpecificIndicatorsController : BaseDropdownViewController<SpecificIndicatorViewModel>
    {
        private readonly IBaseApiService<ConsumptionGroupViewModel> _consumptionGroupService;
        private readonly IBaseApiService<PollutantViewModel> _pollutantService;

        public SpecificIndicatorsController(
            IBaseApiService<SpecificIndicatorViewModel> specificIndicatorService,
            IBaseApiService<ConsumptionGroupViewModel> consumptionGroupService,
            IBaseApiService<PollutantViewModel> pollutantService)
        {
            _apiService = specificIndicatorService;
            _consumptionGroupService = consumptionGroupService;
            _pollutantService = pollutantService;
        }

        protected override string CreationTitle => LangResources.Titles.SpecificIndicatorsCreate;
        protected override string UpdateTitle => LangResources.Titles.SpecificIndicatorsUpdate;

        public override async Task LoadDropdownsValuesAsync(SpecificIndicatorViewModel model)
        {
            var consumptionGroupsResponse = await _consumptionGroupService.GetAllAsync();
            var pollutantsResponse = await _pollutantService.GetAllAsync();

            if (consumptionGroupsResponse.Success && pollutantsResponse.Success)
            {
                model.ConsumptionGroups = consumptionGroupsResponse.Data
                    .Select(cg => new DropdownItemModel { Value = cg.Id, Name = cg.Name })
                    .ToList();

                model.Pollutants = pollutantsResponse.Data
                    .Select(p => new DropdownItemModel { Value = p.Id, Name = p.Name })
                    .ToList();
            }
        }

        [Authorize]
        [LoadLayoutDataFilter]
        [HttpGet("createForConsumptionGroup/{id}")]
        public async Task<IActionResult> CreateForConsumptionGroup([FromRoute] string id)
        {
            var model = new SpecificIndicatorViewModel();

            await LoadDropdownsValuesAsync(model);

            model.ConsumptionGroupId = model.ConsumptionGroups.Any(g => g.Value == id) ? id : null;

            ViewData[AspAction] = nameof(Create);
            ViewData[Title] = CreationTitle;

            return View(Constants.FormView, model);
        }
    }
}
