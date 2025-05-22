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
    public class OperatingTimesController : BaseDropdownViewController<OperatingTimeViewModel>
    {
        private readonly IBaseApiService<EmissionSourceViewModel> _emissionSourceService;

        public OperatingTimesController(IBaseApiService<OperatingTimeViewModel> operatingTimeService, IBaseApiService<EmissionSourceViewModel> emissionSourceService)
        {
            _apiService = operatingTimeService;
            _emissionSourceService = emissionSourceService;
        }

        protected override string CreationTitle => LangResources.Titles.OperatingTimesCreate;
        protected override string UpdateTitle => LangResources.Titles.OperatingTimesUpdate;

        public override async Task LoadDropdownsValuesAsync(OperatingTimeViewModel model)
        {
            var emissionSourcesResponse = await _emissionSourceService.GetAllAsync();

            if (emissionSourcesResponse.Success)
            {
                model.EmissionSources = emissionSourcesResponse.Data
                    .Select(e => new DropdownItemModel { Value = e.Id, Name = e.Name })
                    .ToList();
            }
        }
    }
}
