using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using ServicesConstants = EmisTracking.Services.Constants;

namespace EmisTracking.WebApp.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class LoadLayoutDataFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var user = context.HttpContext.User;

            var viewData = context.Controller is Controller controller ? controller.ViewData : null;

            if (viewData != null)
            {
                var isAuthenticated = user?.Identity?.IsAuthenticated ?? false;

                var roleInfo = isAuthenticated
                    ? user!.Claims.FirstOrDefault(c => c.Type == Constants.ViewDataConstants.RoleInfoClaimType)?.Value
                    : string.Empty;

                var userInfo = isAuthenticated
                    ? user!.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.GivenName)?.Value
                    : string.Empty;

                var isAdmin = isAuthenticated && user!.IsInRole(ServicesConstants.AdminRole);

                viewData[Constants.ViewDataConstants.IsAuthenticated] = isAuthenticated;
                viewData[Constants.ViewDataConstants.RoleInfo] = roleInfo;
                viewData[Constants.ViewDataConstants.UserInfo] = userInfo;
                viewData[Constants.ViewDataConstants.IsAdmin] = isAdmin;
                // FIXME Расширить список ролей!
            }

            base.OnActionExecuting(context);
        }
    }
}
