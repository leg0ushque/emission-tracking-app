using EmisTracking.Localization;
using EmisTracking.Services.WebApi.Services;
using EmisTracking.WebApi.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmisTracking.WebApp.Controllers
{
    [Route("[controller]")]
    [Authorize(Roles = $"{Services.Constants.DirectorRole},{Services.Constants.AccountantRole},{Services.Constants.AdminRole}")]
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
