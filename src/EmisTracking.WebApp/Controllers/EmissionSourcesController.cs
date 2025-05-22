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
    public class EmissionSourcesController : BaseDropdownViewController<EmissionSourceViewModel>
    {
        private readonly IBaseApiService<SubdivisionViewModel> _subdivisionService;
        private readonly IBaseApiService<MethodologyViewModel> _methodologyService;

        public EmissionSourcesController(
            IBaseApiService<EmissionSourceViewModel> emissionSourceService,
            IBaseApiService<SubdivisionViewModel> subdivisionService,
            IBaseApiService<MethodologyViewModel> methodologyService)
        {
            _apiService = emissionSourceService;
            _subdivisionService = subdivisionService;
            _methodologyService = methodologyService;
        }

        protected override string CreationTitle => LangResources.Titles.EmissionSourcesCreate;
        protected override string UpdateTitle => LangResources.Titles.EmissionSourcesUpdate;

        public override async Task LoadDropdownsValuesAsync(EmissionSourceViewModel model)
        {
            var subdivisionsResponse = await _subdivisionService.GetAllAsync();
            var methodologiesResponse = await _methodologyService.GetAllAsync();

            if (subdivisionsResponse.Success && methodologiesResponse.Success)
            {
                model.Subdivisions = subdivisionsResponse.Data
                    .Select(s => new DropdownItemModel { Value = s.Id, Name = s.Name })
                    .ToList();

                model.Methodologies = methodologiesResponse.Data
                    .Select(m => new DropdownItemModel { Value = m.Id, Name = m.Name })
                    .ToList();
            }
        }
    }
}
