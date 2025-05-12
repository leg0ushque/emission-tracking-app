using EmisTracking.Localization;
using EmisTracking.Services.WebApi.Services;
using EmisTracking.WebApi.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmisTracking.WebApp.Controllers
{
    [Route("[controller]")]
    public class SubdivisionsController : BaseViewController<SubdivisionViewModel>
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
                model.Area = areaResponse.Data.Name;

                ViewData[AspAction] = nameof(Create);
                ViewData[Title] = CreationTitle;
                return View(Constants.FormView, model);
            }
            else
            {
                return View(Constants.ErrorView, (areaResponse.ErrorMessage, controller: string.Empty, action: nameof(Index)));
            }
        }

        [Authorize(Roles = Services.Constants.AdminRole)]
        [HttpPost("create")]
        public override async Task<IActionResult> Create([FromForm] SubdivisionViewModel model)
        {
            ViewData[AspAction] = nameof(Create);
            ViewData[Title] = CreationTitle;

            if (!ModelState.IsValid)
            {
                return View(Constants.FormView, model);
            }

            var response = await _apiService.CreateAsync(model);

            if (response.Success)
            {
                return RedirectToAction("Item", "Areas", new { id = model.AreaId });
            }
            else
            {
                UpdateModelStateErrors(ModelState, response.Errors, response.ErrorMessage);

                return View(Constants.FormView, model);
            }
        }

        [Authorize(Roles = Services.Constants.AdminRole)]
        [HttpGet("update/{id}")]
        public override async Task<IActionResult> Update([FromRoute] string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return View(Constants.ErrorView, (LangResources.EmptyIdText, controller: string.Empty, action: nameof(Index)));
            }

            var response = await _apiService.GetByIdAsync(id);


            if (!response.Success)
            {
                return View(Constants.ErrorView, (response.ErrorMessage, controller: string.Empty, action: nameof(Index)));
            }

            var areaResponse = await _areaApiService.GetByIdAsync(id);

            if (areaResponse.Success)
            {
                ViewData[AspAction] = nameof(Update);
                ViewData[Title] = UpdateTitle;

                response.Data.AreaId = id;
                response.Data.Area = areaResponse.Data.Name;

                return View(Constants.FormView, response.Data);
            }
            else
            {
                return View(Constants.ErrorView, (areaResponse.ErrorMessage, controller: string.Empty, action: nameof(Index)));
            }
        }

        [Authorize(Roles = Services.Constants.AdminRole)]
        [HttpPost("update/{id}")]
        public override async Task<IActionResult> Update([FromRoute] string id, [FromForm] SubdivisionViewModel model)
        {
            ViewData[AspAction] = nameof(Update);
            ViewData[Title] = UpdateTitle;

            if (!ModelState.IsValid)
            {
                return View(Constants.FormView, model);
            }

            var response = await _apiService.UpdateAsync(model);

            if (response.Success)
            {
                return RedirectToAction("Item", "Areas", new { id = model.AreaId });
            }
            else
            {
                UpdateModelStateErrors(ModelState, response.Errors, response.ErrorMessage);

                return View(Constants.FormView, model);
            }
        }

        [Authorize(Roles = Services.Constants.AdminRole)]
        [HttpGet("delete/{id}")]
        public override async Task<IActionResult> Delete([FromRoute] string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return View(Constants.ErrorView, (LangResources.EmptyIdText, controller: string.Empty, action: nameof(Index)));
            }

            var response = await _apiService.GetByIdAsync(id);

            if (response.Success)
            {
                return View(response.Data);
            }
            else
            {
                return View(Constants.ErrorView, (response.ErrorMessage, controller: string.Empty, action: nameof(Index)));
            }
        }

        [Authorize(Roles = Services.Constants.AdminRole)]
        [HttpGet("confirm-delete/{id}")]
        public override async Task<IActionResult> ConfirmDelete([FromRoute] string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return View(Constants.ErrorView, (LangResources.EmptyIdText, controller: string.Empty, action: nameof(Index)));
            }

            var response = await _apiService.DeleteByIdAsync(id);

            if (response.Success)
            {
                return RedirectToAction(nameof(Item));
            }
            else
            {
                return View(Constants.ErrorView, (response.ErrorMessage, controller: string.Empty, action: nameof(Index)));
            }
        }
    }
}
