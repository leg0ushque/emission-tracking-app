using EmisTracking.Localization;
using EmisTracking.Services.WebApi.Services;
using EmisTracking.WebApi.Models.Models;
using EmisTracking.WebApi.Models.ViewModels;
using EmisTracking.WebApp.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace EmisTracking.WebApp.Controllers
{
    [LoadLayoutDataFilter]
    [Route("[controller]")]
    public class ConsumptionGroupsController : BaseDropdownViewController<ConsumptionGroupViewModel>
    {
        private readonly IBaseApiService<MethodologyViewModel> _methodologyService;

        public ConsumptionGroupsController(IBaseApiService<ConsumptionGroupViewModel> consumptionGroupService, IBaseApiService<MethodologyViewModel> methodologyService)
        {
            _apiService = consumptionGroupService;
            _methodologyService = methodologyService;
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
    }
}
