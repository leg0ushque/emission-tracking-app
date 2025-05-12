using EmisTracking.Localization;
using EmisTracking.Services.WebApi.Services;
using EmisTracking.WebApi.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EmisTracking.WebApp.Controllers
{
    [Route("[controller]")]
    public class TaxesController : BaseViewController<TaxViewModel>
    {
        public TaxesController(IBaseApiService<TaxViewModel> apiService)
        {
            _apiService = apiService;
        }

        protected override string CreationTitle => LangResources.Titles.TaxesCreate;
        protected override string UpdateTitle => LangResources.Titles.TaxesUpdate;
    }
}
