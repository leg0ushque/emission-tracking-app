using EmisTracking.Localization;
using EmisTracking.Services.WebApi.Services;
using EmisTracking.WebApi.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EmisTracking.WebApp.Controllers
{
    [Route("[controller]")]
    public class OperatingTimesController : BaseViewController<OperatingTimeViewModel>
    {
        public OperatingTimesController(IBaseApiService<OperatingTimeViewModel> apiService)
        {
            _apiService = apiService;
        }

        protected override string CreationTitle => LangResources.Titles.OperatingTimesCreate;
        protected override string UpdateTitle => LangResources.Titles.OperatingTimesUpdate;
    }
}
