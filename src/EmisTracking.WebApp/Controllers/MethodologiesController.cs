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
    public class MethodologiesController : BaseDropdownViewController<MethodologyViewModel>
    {
        private readonly IBaseApiService<ModeViewModel> _modeService;

        public MethodologiesController(IBaseApiService<MethodologyViewModel> methodologyService, IBaseApiService<ModeViewModel> modeService)
        {
            _apiService = methodologyService;
            _modeService = modeService;
        }

        protected override string CreationTitle => LangResources.Titles.MethodologiesCreate;
        protected override string UpdateTitle => LangResources.Titles.MethodologiesUpdate;

        public override async Task LoadDropdownsValuesAsync(MethodologyViewModel model)
        {
            var modesResponse = await _modeService.GetAllAsync();

            if (modesResponse.Success)
            {
                model.Modes = modesResponse.Data
                    .Select(m => new DropdownItemModel { Value = m.Id, Name = m.Name })
                    .ToList();
            }
        }
    }
}
