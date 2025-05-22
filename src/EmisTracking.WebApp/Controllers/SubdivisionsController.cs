using EmisTracking.Localization;
using EmisTracking.Services.Services;
using EmisTracking.Services.WebApi.Services;
using EmisTracking.WebApi.Models.Models;
using EmisTracking.WebApi.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace EmisTracking.WebApp.Controllers
{
    [Route("[controller]")]
    public class SubdivisionsController : BaseDropdownViewController<SubdivisionViewModel>
    {
        private readonly IBaseApiService<AreaViewModel> _areaApiService;

        public SubdivisionsController(
            IBaseApiService<AreaViewModel> areaApiService,
            IBaseApiService<SubdivisionViewModel> apiService,
            ISubdivisionApiService subdivisionsApiService)
        {
            _apiService = apiService;
            _areaApiService = areaApiService;
        }

        protected override string CreationTitle => LangResources.Titles.SubdivisionsCreate;
        protected override string UpdateTitle => LangResources.Titles.SubdivisionsUpdate;

        [Authorize(Roles = Services.Constants.AdminRole)]
        [HttpGet("createForArea/{id}")]
        public async Task<IActionResult> CreateForArea([FromRoute] string id)
        {
            var model = new SubdivisionViewModel();

            var areaResponse = await _areaApiService.GetByIdAsync(id);

            if (areaResponse.Success)
            {
                model.AreaId = id;
                model.Area = areaResponse.Data;

                ViewData[AspAction] = nameof(Create);
                ViewData[Title] = CreationTitle;
                return View(Constants.FormView, model);
            }
            else
            {
                return View(Constants.ErrorView, (areaResponse.ErrorMessage, controller: string.Empty, action: nameof(Index)));
            }
        }
        public override async Task LoadDropdownsValuesAsync(SubdivisionViewModel model)
        {
            var areasResponse = await _areaApiService.GetAllAsync();
            if (areasResponse.Success)
            {
                model.Areas = areasResponse.Data
                    .Select(a => new DropdownItemModel { Value = a.Id, Name = a.Name })
                    .ToList();
            }
        }
    }
}
