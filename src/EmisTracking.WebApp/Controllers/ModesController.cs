using EmisTracking.Localization;
using EmisTracking.Services.WebApi.Services;
using EmisTracking.WebApi.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EmisTracking.WebApp.Controllers
{
    [Route("[controller]")]
    [Authorize(Roles = Services.Constants.AdminRole)]
    public class ModesController : BaseViewController<ModeViewModel>
    {
        public ModesController(IBaseApiService<ModeViewModel> apiService)
        {
            _apiService = apiService;
        }

        protected override string CreationTitle => LangResources.Titles.ModesCreate;
        protected override string UpdateTitle => LangResources.Titles.ModesUpdate;
    }
}
