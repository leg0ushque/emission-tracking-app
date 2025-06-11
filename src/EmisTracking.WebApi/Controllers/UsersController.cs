using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using EmisTracking.Localization;
using EmisTracking.Services;
using EmisTracking.Services.Entities;
using EmisTracking.Services.JwtAuth;
using EmisTracking.Services.Services;
using EmisTracking.WebApi.Filters;
using EmisTracking.WebApi.Models.Models;
using EmisTracking.WebApi.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmisTracking.WebApi.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : JwtBaseController
    {
        private readonly IUsersService _service;
        private readonly IMapper _mapper;

        protected string EntityName => LangResources.Entities.User;

        public UsersController(IUsersService usersService,
            SignInManager<SystemUser> signInManager,
            UserManager<SystemUser> userManager,
            JwtTokenService jwtTokenService,
            IMapper mapper)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _jwtTokenService = jwtTokenService;

            _service = usersService;
            _usersService = usersService;
            _mapper = mapper;
        }

        [Authorize(Roles = Constants.AdminRole)]
        [HttpGet("")]
        [BusinessLogicExceptionFilter]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAll()
        {
            var allUsers = await _service.GetAllFullUsers();

            var userList = _mapper.Map<List<UserViewModel>>(allUsers);

            return Ok(new ApiResponseModel<List<UserViewModel>> { Success = true, Data = userList });
        }

        [Authorize(Roles = Constants.AdminRole)]
        [HttpGet("byRole/{roleName}")]
        [BusinessLogicExceptionFilter]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllByRole([FromRoute] string roleName)
        {
            if (string.IsNullOrEmpty(roleName))
            {
                return BadRequest(new ApiResponseModel<object>
                {
                    Success = false,
                    ErrorMessage = LangResources.EmptyIdText,
                });
            }

            var roleFullUsers = await _service.GetByRoleAsync(roleName);

            var userViewModelList = _mapper.Map<List<UserViewModel>>(roleFullUsers);

            return Ok(new ApiResponseModel<List<UserViewModel>> { Success = true, Data = userViewModelList });
        }

        [HttpPost("register")]
        [BusinessLogicExceptionFilter]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (model.Role != Constants.AccountantRole
                && model.Role != Constants.EcologistRole
                && model.Role != Constants.OperatorRole
                && model.Role != Constants.DirectorRole)
            {
                ModelState.AddModelError(string.Empty, LangResources.IncorrectRole);

                return CreateBadRequestResponse(ModelState);
            }

            var systemUser = new SystemUser()
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = model.FirstName,
                MiddleName = model.MiddleName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.Email,
                NormalizedUserName = model.Email,
                NormalizedEmail = model.Email,
                EmailConfirmed = true,
                LockoutEnabled = true,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };

            var creationResult = await CreateUserAsync(systemUser, model.Password, model.Role);

            return creationResult.Success ? Ok(creationResult) : BadRequest(creationResult);
        }

        private async Task<ApiResponseModel<string>> CreateUserAsync(SystemUser systemUser, string password, string role)
        {
            var result = new ApiResponseModel<string>();

            var hasher = new PasswordHasher<SystemUser>();
            systemUser.PasswordHash = hasher.HashPassword(systemUser, password);

            var creationResult = await _userManager.CreateAsync(systemUser, password);

            if (creationResult.Succeeded)
            {
                await _userManager.AddToRoleAsync(systemUser, role);

                result.Success = true;

                return result;
            }

            var messages = creationResult.Errors.Select(x => x.Description).ToArray();

            result.Success = false;
            result.ErrorMessage = string.Join(". ", messages);

            return result;
        }

        [Authorize]
        [HttpPost("")]
        [BusinessLogicExceptionFilter]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public virtual async Task<IActionResult> Create([FromBody] UserViewModel item)
        {
            if (item is null)
            {
                return CreateBadRequestResponse(ModelState);
            }

            var itemDto = _mapper.Map<User>(item);
            itemDto.Id = Guid.NewGuid().ToString();

            var result = await _service.AddAsync(itemDto);

            return Ok(new ApiResponseModel<string>
            {
                Success = true,
                StatusCode = System.Net.HttpStatusCode.Created,
                Data = result
            });
        }

        [Authorize]
        [HttpGet("{id}")]
        [BusinessLogicExceptionFilter]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetById([FromRoute] string id, [FromQuery] bool loadDependencies = false)
        {
            var user = _service.GetByIdAsync(id);
            var mappedUserViewModel = _mapper.Map<UserViewModel>(user);

            return Ok(new ApiResponseModel<UserViewModel> { Success = true, Data = mappedUserViewModel });
        }

        [Authorize]
        [HttpPut("")]
        [BusinessLogicExceptionFilter]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Update([FromBody] UserViewModel item)
        {
            if (item is null)
            {
                return CreateBadRequestResponse(ModelState);
            }

            var itemDto = _mapper.Map<User>(item);
            var updateSuccessful = await _service.UpdateAsync(itemDto);

            return updateSuccessful ?
                Ok(new ApiResponseModel<object> { Success = true })
                : NotFound(new ApiResponseModel<object>
                {
                    Success = false,
                    StatusCode = System.Net.HttpStatusCode.NotFound,
                    ErrorMessage = string.Format(LangResources.ItemNotFoundMessageTemplate, EntityName, item.Id)
                });
        }

        [Authorize]
        [HttpDelete("{id}")]
        [BusinessLogicExceptionFilter]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            var deleteSuccessful = await _service.DeleteAsync(id);

            return deleteSuccessful ?
                Ok(new ApiResponseModel<object> { Success = true })
                : NotFound(new ApiResponseModel<object>
                {
                    Success = false,
                    StatusCode = System.Net.HttpStatusCode.NotFound,
                    ErrorMessage = string.Format(LangResources.ItemNotFoundMessageTemplate, EntityName, id)
                });
        }
    }
}
