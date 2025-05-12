using EmisTracking.Localization;
using EmisTracking.Services.WebApi.Services;
using EmisTracking.WebApi.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EmisTracking.WebApp.Controllers
{
    [Route("[controller]")]
    public class SpecificIndicatorsController : BaseViewController<SpecificIndicatorViewModel>
    {
        public SpecificIndicatorsController(IBaseApiService<SpecificIndicatorViewModel> apiService)
        {
            _apiService = apiService;
        }

        protected override string CreationTitle => LangResources.Titles.SpecificIndicatorsCreate;
        protected override string UpdateTitle => LangResources.Titles.SpecificIndicatorsUpdate;
    }
}
