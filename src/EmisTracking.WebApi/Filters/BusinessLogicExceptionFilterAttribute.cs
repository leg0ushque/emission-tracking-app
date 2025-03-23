using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using EmisTracking.WebApi.Models.Models;
using System;

namespace EmisTracking.WebApi.Filters
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class BusinessLogicExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is Services.Exceptions.BusinessLogicException ex)
            {
                var code = HttpStatusCode.BadRequest;

                context.ExceptionHandled = true;
                context.HttpContext.Response.StatusCode = (int)code;
                context.Result = new ObjectResult(new ErrorResponseModel()
                {
                    StatusCode = code,
                    Message = ex.InnerException.Message,
                });

                base.OnException(context);
            }
        }
    }
}
