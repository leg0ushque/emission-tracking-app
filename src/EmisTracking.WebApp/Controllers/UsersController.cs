using EmisTracking.Localization;
using EmisTracking.Services.WebApi.Services;
using EmisTracking.WebApi.Models.Models;
using EmisTracking.WebApi.Models.ViewModels;
using EmisTracking.WebApp.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmisTracking.WebApp.Controllers
{
    [Route("[controller]")]
    [LoadLayoutDataFilter]
    [Authorize(Roles = $"{Services.Constants.DirectorRole},{Services.Constants.AdminRole}}")]
    public class UsersController : BaseViewController<UserViewModel>
    {
        private readonly IUserApiService _userService;

        public UsersController(
            IUserApiService userService)
        {
            _userService = userService;
            _apiService = userService;
        }

        protected override string CreationTitle => LangResources.Titles.UsersCreate;
        protected override string UpdateTitle => LangResources.Titles.UsersUpdate;

        [HttpGet("register")]
        public IActionResult Register()
        {
            var model = new RegisterModel()
            {
                Roles = CreateRolesList()
            };

            return View(model);
        }

        [HttpPost("register")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([FromForm] RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var response = await _userService.PostRegisterAsync(model);

            if (response.Success)
            {
                return RedirectToAction("Index", "Users");
            }
            else
            {
                UpdateModelStateErrors(ModelState, response.Errors, response.ErrorMessage);

                model.Roles = CreateRolesList();

                return View(model);
            }
        }

        private List<DropdownItemModel> CreateRolesList() =>
        [
            new(Services.Constants.EcologistRole, Services.Constants.EcologistRole),
                new(Services.Constants.AccountantRole, Services.Constants.AccountantRole),
                new(Services.Constants.OperatorRole, Services.Constants.OperatorRole),
                new(Services.Constants.DirectorRole, Services.Constants.DirectorRole),
        ];
    }
}
