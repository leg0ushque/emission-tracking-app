using EmisTracking.Localization;
using EmisTracking.Services.WebApi.Services;
using EmisTracking.WebApi.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EmisTracking.WebApp.Controllers
{
    [Route("[controller]")]
    public class GrossEmissionsController : BaseViewController<GrossEmissionViewModel>
    {
        public GrossEmissionsController(IBaseApiService<GrossEmissionViewModel> apiService)
        {
            _apiService = apiService;
        }

        protected override string CreationTitle => LangResources.Titles.GrossEmissionsCreate;
        protected override string UpdateTitle => LangResources.Titles.GrossEmissionsUpdate;
    }
}
