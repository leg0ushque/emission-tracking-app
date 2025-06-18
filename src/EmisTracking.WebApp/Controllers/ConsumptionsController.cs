using EmisTracking.Localization;
using EmisTracking.Services.WebApi.Services;
using EmisTracking.WebApi.Models.Models;
using EmisTracking.WebApi.Models.ViewModels;
using EmisTracking.WebApp.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EmisTracking.WebApp.Controllers
{
    [LoadLayoutDataFilter]
    [Route("[controller]")]
    [Authorize(Roles = $"{Services.Constants.EcologistRole},{Services.Constants.AdminRole}")]
    public class ConsumptionsController : BaseDropdownViewController<ConsumptionViewModel>
    {
        private readonly IBaseApiService<ConsumptionGroupViewModel> _consumptionGroupService;
        private readonly IBaseApiService<MethodologyViewModel> _methodologyService;

        public ConsumptionsController(IBaseApiService<ConsumptionViewModel> consumptionService, IBaseApiService<ConsumptionGroupViewModel> consumptionGroupService, IBaseApiService<MethodologyViewModel> methodologyService)
        {
            _apiService = consumptionService;
            _consumptionGroupService = consumptionGroupService;
            _methodologyService = methodologyService;
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

        [Authorize]
        [LoadLayoutDataFilter]
        [HttpGet("createForMethodologyGroups/{id}")]
        public async Task<IActionResult> CreateForMethodologyGroups([FromRoute] string id,
            [FromQuery] int? month,
            [FromQuery] int? year)
        {
            var methodologyResponse = await _methodologyService.GetByIdAsync(id);

            ViewData[AspAction] = nameof(Create);
            ViewData[Title] = $"{CreationTitle} для расходных групп методики" +
                (methodologyResponse.Success ? $" \"{methodologyResponse.Data.ShortName}\"" : string.Empty);

            var model = new ConsumptionViewModel();

            var consumptionGroupsResponse = await _consumptionGroupService.GetAllAsync();

            if (consumptionGroupsResponse.Success)
            {
                model.ConsumptionGroups = consumptionGroupsResponse.Data
                    .Where(g => g.MethodologyId == id)
                    .Select(cg => new DropdownItemModel { Value = cg.Id, Name = cg.Name })
                    .ToList();
            }

            var currentDate = DateTime.Now;

            model.Month = month.HasValue ? month.Value : currentDate.Month;
            model.Year = year.HasValue ? year.Value : currentDate.Year;

            return View(Constants.FormView, model);
        }
    }
}
