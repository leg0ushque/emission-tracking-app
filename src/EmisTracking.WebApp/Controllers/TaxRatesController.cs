using EmisTracking.Localization;
using EmisTracking.Services.WebApi.Services;
using EmisTracking.WebApi.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmisTracking.WebApp.Controllers
{
    [Route("[controller]")]
    [Authorize(Roles = $"{Services.Constants.DirectorRole},{Services.Constants.AccountantRole},{Services.Constants.AdminRole}}")]
    public class TaxRatesController : BaseViewController<TaxRateViewModel>
    {
        public TaxRatesController(IBaseApiService<TaxRateViewModel> apiService)
        {
            _apiService = apiService;
        }

        protected override string CreationTitle => LangResources.Titles.TaxRatesCreate;
        protected override string UpdateTitle => LangResources.Titles.TaxRatesUpdate;
    }
}
