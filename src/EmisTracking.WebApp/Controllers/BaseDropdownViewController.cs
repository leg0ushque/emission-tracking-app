using EmisTracking.Localization;
using EmisTracking.WebApi.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EmisTracking.WebApp.Controllers
{
    public abstract class BaseDropdownViewController<TEntityViewModel> : BaseViewController<TEntityViewModel>
        where TEntityViewModel : class, IViewModel, new()
    {
        public abstract Task LoadDropdownsValuesAsync(TEntityViewModel model);

        [HttpGet("create")]
        public override async Task<IActionResult> Create()
        {
            var model = new TEntityViewModel();

            await LoadDropdownsValuesAsync(model);

            ViewData[AspAction] = nameof(Create);
            ViewData[Title] = CreationTitle;

            return View(Constants.FormView, model);
        }

        [HttpPost("create")]
        public override async Task<IActionResult> Create([FromForm] TEntityViewModel model)
        {
            ViewData[AspAction] = nameof(Create);
            ViewData[Title] = CreationTitle;

            if (!ModelState.IsValid)
            {
                await LoadDropdownsValuesAsync(model);

                return View(Constants.FormView, model);
            }

            var response = await _apiService.CreateAsync(model);
            if (response.Success)
            {
                return RedirectToAction(nameof(Item), new { id = response.Data });
            }
            else
            {
                UpdateModelStateErrors(ModelState, response.Errors, response.ErrorMessage);

                await LoadDropdownsValuesAsync(model);

                return View(Constants.FormView, model);
            }
        }

        [HttpGet("update/{id}")]
        public override async Task<IActionResult> Update([FromRoute] string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return View(Constants.ErrorView, (LangResources.EmptyIdText, controller: string.Empty, action: nameof(Index)));
            }

            var response = await _apiService.GetByIdAsync(id, loadDependencies: true);

            ViewData[AspAction] = nameof(Update);
            ViewData[Title] = UpdateTitle;

            if (response.Success)
            {
                var model = response.Data;

                await LoadDropdownsValuesAsync(model);

                return View(Constants.FormView, response.Data);
            }
            else
            {
                return View(Constants.ErrorView, (response.ErrorMessage, controller: string.Empty, action: nameof(Index)));
            }
        }

        [HttpPost("update/{id}")]
        public override async Task<IActionResult> Update([FromRoute] string id, [FromForm] TEntityViewModel model)
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
                return RedirectToAction(nameof(Item), new { id });
            }
            else
            {
                await LoadDropdownsValuesAsync(model);

                UpdateModelStateErrors(ModelState, response.Errors, response.ErrorMessage);

                return View("Form", model);
            }
        }
    }
}
