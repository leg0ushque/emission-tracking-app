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
    public class ParameterValuesController : BaseDropdownViewController<ParameterValueViewModel>
    {
        private readonly IBaseApiService<MethodologyParameterViewModel> _methodologyParameterService;
        private readonly IBaseApiService<GrossEmissionViewModel> _grossEmissionService;

        public ParameterValuesController(
            IBaseApiService<ParameterValueViewModel> parameterValueService,
            IBaseApiService<MethodologyParameterViewModel> methodologyParameterService,
            IBaseApiService<GrossEmissionViewModel> grossEmissionService)
        {
            _apiService = parameterValueService;
            _methodologyParameterService = methodologyParameterService;
            _grossEmissionService = grossEmissionService;
        }

        protected override string CreationTitle => LangResources.Titles.ParameterValuesCreate;
        protected override string UpdateTitle => LangResources.Titles.ParameterValuesUpdate;

        public override async Task LoadDropdownsValuesAsync(ParameterValueViewModel model)
        {
            var methodologyParametersResponse = await _methodologyParameterService.GetAllAsync();
            var grossEmissionsResponse = await _grossEmissionService.GetAllAsync();

            if (methodologyParametersResponse.Success && grossEmissionsResponse.Success)
            {
                model.MethodologyParameters = methodologyParametersResponse.Data
                    .Select(mp => new DropdownItemModel { Value = mp.Id, Name = mp.Name })
                    .ToList();

                model.GrossEmissions = grossEmissionsResponse.Data
                    .Select(ge => new DropdownItemModel { Value = ge.Id, Name = $"{ge.Mass} kg - {ge.Month}/{ge.Year}" })
                    .ToList();
            }
        }
    }

}
