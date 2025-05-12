using EmisTracking.Localization;
using EmisTracking.Services.WebApi.Services;
using EmisTracking.WebApi.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EmisTracking.WebApp.Controllers
{
    [Route("[controller]")]
    public class ConsumptionGroupsController : BaseViewController<ConsumptionGroupViewModel>
    {
        public ConsumptionGroupsController(IBaseApiService<ConsumptionGroupViewModel> apiService)
        {
            _apiService = apiService;
        }

        protected override string CreationTitle => LangResources.Titles.ConsumptionGroupsCreate;
        protected override string UpdateTitle => LangResources.Titles.ConsumptionGroupsUpdate;
    }
}
