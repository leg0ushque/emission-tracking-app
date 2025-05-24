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
    [Route("[controller]")]
    public class SubdivisionsController : BaseDropdownViewController<SubdivisionViewModel>
    {
        private readonly IBaseApiService<AreaViewModel> _areaApiService;
        private readonly ISubdivisionApiService subdivisionsApiService;

        public SubdivisionsController(
            IBaseApiService<AreaViewModel> areaApiService,
            IBaseApiService<SubdivisionViewModel> apiService,
            ISubdivisionApiService subdivisionsApiService)
        {
            _apiService = apiService;
            _areaApiService = areaApiService;
            this.subdivisionsApiService = subdivisionsApiService;
        }

        protected override string CreationTitle => LangResources.Titles.SubdivisionsCreate;
        protected override string UpdateTitle => LangResources.Titles.SubdivisionsUpdate;

        [Authorize(Roles = Services.Constants.AdminRole)]
        [HttpGet("createForArea/{id}")]
        public async Task<IActionResult> CreateForArea([FromRoute] string id)
        {
            ViewData[AspAction] = nameof(Create);
            ViewData[Title] = CreationTitle;

            var model = new SubdivisionViewModel();

            await LoadDropdownsValuesAsync(model);

            model.AreaId = model.Areas.Any(a => a.Value == id) ? id : null;

            return View(Constants.FormView, model);
        }


        [Authorize(Roles = Services.Constants.AdminRole)]
        [LoadLayoutDataFilter]
        [HttpGet("confirm-delete/{id}")]
        public override async Task<IActionResult> ConfirmDelete([FromRoute] string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return View(Constants.ErrorView, (LangResources.EmptyIdText, controller: string.Empty, action: nameof(Index)));
            }

            var existingItemResponse = await _apiService.GetByIdAsync(id);

            if(!existingItemResponse.Success)
            {
                return View(Constants.ErrorView, (existingItemResponse.ErrorMessage, controller: string.Empty, action: nameof(Index)));
            }

            var response = await _apiService.DeleteByIdAsync(id);

            if (response.Success)
            {
                return RedirectToAction("Item", "Area", new { id = existingItemResponse.Data.AreaId });
            }
            else
            {
                return View(Constants.ErrorView, (response.ErrorMessage, controller: string.Empty, action: nameof(Index)));
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
