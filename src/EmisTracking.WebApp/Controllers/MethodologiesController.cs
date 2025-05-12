using EmisTracking.Localization;
using EmisTracking.Services.WebApi.Services;
using EmisTracking.WebApi.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EmisTracking.WebApp.Controllers
{
    [Route("[controller]")]
    public class MethodologiesController : BaseViewController<MethodologyViewModel>
    {
        public MethodologiesController(IBaseApiService<MethodologyViewModel> apiService)
        {
            _apiService = apiService;
        }

        protected override string CreationTitle => LangResources.Titles.MethodologiesCreate;
        protected override string UpdateTitle => LangResources.Titles.MethodologiesUpdate;
    }
}
