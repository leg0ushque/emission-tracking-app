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
    public class GrossEmissionsController : BaseDropdownViewController<GrossEmissionViewModel>
    {
        private readonly IBaseApiService<SourceSubstanceViewModel> _sourceSubstanceService;
        private readonly IBaseApiService<MethodologyViewModel> _methodologyService;
        private readonly IBaseApiService<TaxViewModel> _taxService;

        public GrossEmissionsController(
            IBaseApiService<GrossEmissionViewModel> grossEmissionService,
            IBaseApiService<SourceSubstanceViewModel> sourceSubstanceService,
            IBaseApiService<MethodologyViewModel> methodologyService,
            IBaseApiService<TaxViewModel> taxService)
        {
            _apiService = grossEmissionService;
            _sourceSubstanceService = sourceSubstanceService;
            _methodologyService = methodologyService;
            _taxService = taxService;
        }

        protected override string CreationTitle => LangResources.Titles.GrossEmissionsCreate;
        protected override string UpdateTitle => LangResources.Titles.GrossEmissionsUpdate;

        public override async Task LoadDropdownsValuesAsync(GrossEmissionViewModel model)
        {
            var sourceSubstancesResponse = await _sourceSubstanceService.GetAllAsync();
            var methodologiesResponse = await _methodologyService.GetAllAsync();
            var taxesResponse = await _taxService.GetAllAsync();

            if (sourceSubstancesResponse.Success && methodologiesResponse.Success && taxesResponse.Success)
            {
                model.SourceSubstances = sourceSubstancesResponse.Data
                    .Select(ss => new DropdownItemModel { Value = ss.Id, Name = ss.EmissionSourceId })
                    .ToList();

                model.Methodologies = methodologiesResponse.Data
                    .Select(m => new DropdownItemModel { Value = m.Id, Name = m.Name })
                    .ToList();

                model.Taxes = taxesResponse.Data
                    .Select(t => new DropdownItemModel { Value = t.Id, Name = t.TotalAmount.ToString() })
                    .ToList();
            }
        }
    }
}
