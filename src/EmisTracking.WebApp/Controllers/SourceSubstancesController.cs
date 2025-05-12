using EmisTracking.Localization;
using EmisTracking.Services.WebApi.Services;
using EmisTracking.WebApi.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EmisTracking.WebApp.Controllers
{
    [Route("[controller]")]
    public class SourceSubstancesController : BaseViewController<SourceSubstanceViewModel>
    {
        public SourceSubstancesController(IBaseApiService<SourceSubstanceViewModel> apiService)
        {
            _apiService = apiService;
        }

        protected override string CreationTitle => LangResources.Titles.SourceSubstancesCreate;
        protected override string UpdateTitle => LangResources.Titles.SourceSubstancesUpdate;
    }
}
