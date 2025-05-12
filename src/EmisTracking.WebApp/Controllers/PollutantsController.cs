using EmisTracking.Localization;
using EmisTracking.Services.WebApi.Services;
using EmisTracking.WebApi.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EmisTracking.WebApp.Controllers
{
    [Route("[controller]")]
    public class PollutantsController : BaseViewController<PollutantViewModel>
    {
        public PollutantsController(IBaseApiService<PollutantViewModel> apiService)
        {
            _apiService = apiService;
        }

        protected override string CreationTitle => LangResources.Titles.PollutantsCreate;
        protected override string UpdateTitle => LangResources.Titles.PollutantsUpdate;
    }
}
