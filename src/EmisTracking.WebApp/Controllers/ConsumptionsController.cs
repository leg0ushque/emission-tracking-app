using EmisTracking.Localization;
using EmisTracking.Services.WebApi.Services;
using EmisTracking.WebApi.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EmisTracking.WebApp.Controllers
{
    [Route("[controller]")]
    public class ConsumptionsController : BaseViewController<ConsumptionViewModel>
    {
        public ConsumptionsController(IBaseApiService<ConsumptionViewModel> apiService)
        {
            _apiService = apiService;
        }

        protected override string CreationTitle => LangResources.Titles.ConsumptionsCreate;
        protected override string UpdateTitle => LangResources.Titles.ConsumptionsUpdate;
    }
}
