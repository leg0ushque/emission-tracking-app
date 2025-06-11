using EmisTracking.Localization;
using EmisTracking.Services.WebApi.Services;
using EmisTracking.WebApi.Models.Models;
using EmisTracking.WebApi.Models.ViewModels;
using EmisTracking.WebApp.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Threading.Tasks;

namespace EmisTracking.WebApp.Controllers
{
    public abstract class BaseViewController<TEntityViewModel> : Controller
        where TEntityViewModel : class, IViewModel, new()
    {
        public const string AspAction = "AspAction";
        public const string Title = "Title";

        protected IBaseApiService<TEntityViewModel> _apiService;

        protected abstract string CreationTitle { get; }
        protected abstract string UpdateTitle { get; }

        [Authorize]
        [LoadLayoutDataFilter]
        [HttpGet("")]
        public virtual async Task<IActionResult> Index()
        {
            var response = await _apiService.GetAllAsync(loadDependencies: true);

            if (response.Success)
            {
                return View(response.Data);
            }
            else
            {
                return View(Constants.ErrorView, (errorMessage: response.ErrorMessage, controller: string.Empty, action: nameof(Index)));
            }
        }

        [Authorize]
        [LoadLayoutDataFilter]
        [HttpGet("{id}")]
        public virtual async Task<IActionResult> Item([FromRoute] string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return View(Constants.ErrorView, (LangResources.EmptyIdText, controller: string.Empty, action: nameof(Index)));
            }

            var response = await _apiService.GetByIdAsync(id, loadDependencies: true);

            if (response.Success)
            {
                return View(response.Data);
            }
            else
            {
                return View(Constants.ErrorView, (errorMessage: response.ErrorMessage, controller: string.Empty, action: nameof(Index)));
            }
        }

        [Authorize(Roles = Services.Constants.AdminRole)]
        [LoadLayoutDataFilter]
        [HttpGet("create")]
        public virtual Task<IActionResult> Create()
        {
            var model = new TEntityViewModel();

            ViewData[AspAction] = nameof(Create);
            ViewData[Title] = CreationTitle;
            return Task.FromResult<IActionResult>(View(Constants.FormView, model));
        }

        [Authorize(Roles = Services.Constants.AdminRole)]
        [LoadLayoutDataFilter]
        [HttpPost("create")]
        public virtual async Task<IActionResult> Create([FromForm] TEntityViewModel model)
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
                return RedirectToAction(nameof(Item), new { id = response.Data });
            }
            else
            {
                UpdateModelStateErrors(ModelState, response.Errors, response.ErrorMessage);

                return View(Constants.FormView, model);
            }
        }

        [Authorize(Roles = Services.Constants.AdminRole)]
        [LoadLayoutDataFilter]
        [HttpGet("update/{id}")]
        public virtual async Task<IActionResult> Update([FromRoute] string id)
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
                return View(Constants.FormView, response.Data);
            }
            else
            {
                return View(Constants.ErrorView, (response.ErrorMessage, controller: string.Empty, action: nameof(Index)));
            }
        }

        [Authorize(Roles = Services.Constants.AdminRole)]
        [LoadLayoutDataFilter]
        [HttpPost("update/{id}")]
        public virtual async Task<IActionResult> Update([FromRoute] string id, [FromForm] TEntityViewModel model)
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
                UpdateModelStateErrors(ModelState, response.Errors, response.ErrorMessage);

                return View(Constants.FormView, model);
            }
        }

        [Authorize(Roles = Services.Constants.AdminRole)]
        [LoadLayoutDataFilter]
        [HttpGet("delete/{id}")]
        public virtual async Task<IActionResult> Delete([FromRoute] string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return View(Constants.ErrorView, (LangResources.EmptyIdText, controller: string.Empty, action: nameof(Index)));
            }

            var response = await _apiService.GetByIdAsync(id, loadDependencies: true);

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
        [LoadLayoutDataFilter]
        [HttpGet("confirm-delete/{id}")]
        public virtual async Task<IActionResult> ConfirmDelete([FromRoute] string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return View(Constants.ErrorView, (LangResources.EmptyIdText, controller: string.Empty, action: nameof(Index)));
            }

            var response = await _apiService.DeleteByIdAsync(id);

            if (response.Success)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(Constants.ErrorView, (response.ErrorMessage, controller: string.Empty, action: nameof(Index)));
            }
        }

        protected static void UpdateModelStateErrors(ModelStateDictionary modelState, FieldErrorModel[] errors, string errorMessage = null)
        {
            if (errorMessage != null)
            {
                modelState.AddModelError(string.Empty, errorMessage);
            }

            if (errors == null || errors.Length == 0)
            {
                return;
            }

            foreach (var error in errors)
            {
                modelState.AddModelError(error.Field, error.Message);
            }
        }
    }
}
