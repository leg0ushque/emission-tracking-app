﻿using EmisTracking.Localization;
using EmisTracking.Services.WebApi.Services;
using EmisTracking.WebApi.Models.ViewModels;
using EmisTracking.WebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EmisTracking.WebApp.Controllers
{
    [Authorize(Roles =$"{Services.Constants.OperatorRole},{Services.Constants.AdminRole}")]
    [Route("[controller]")]
    public class AreasController : BaseViewController<AreaViewModel>
    {
        private readonly ISubdivisionApiService _subdivisionsApiService;

        public AreasController(IBaseApiService<AreaViewModel> apiService, ISubdivisionApiService subdivisionsApiService)
        {
            _apiService = apiService;
            _subdivisionsApiService = subdivisionsApiService;
        }

        protected override string CreationTitle => LangResources.Titles.AreasCreate;
        protected override string UpdateTitle => LangResources.Titles.AreasUpdate;

        [Authorize]
        [HttpGet("{id}")]
        public override async Task<IActionResult> Item([FromRoute] string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return View(Constants.ErrorView, (LangResources.EmptyIdText, controller: string.Empty, action: nameof(Index)));
            }

            var response = await _apiService.GetByIdAsync(id);

            if (response.Success)
            {
                var model = new ModelWithDependencies<AreaViewModel,SubdivisionViewModel> { MainItem = response.Data };

                var subdivisionsResponse = await _subdivisionsApiService.GetAllByAreaIdAsync(id);

                model.Dependencies = subdivisionsResponse.Success ? subdivisionsResponse.Data : [];

                return View(model);
            }
            else
            {
                return View(Constants.ErrorView, (errorMessage: response.ErrorMessage, controller: string.Empty, action: nameof(Index)));
            }
        }
    }
}

