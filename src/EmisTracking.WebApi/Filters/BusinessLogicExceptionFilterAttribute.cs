using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using EmisTracking.WebApi.Models.Models;
using System;
using EmisTracking.Localization.StudentsPerf.Localization;
using System.Linq;

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
                context.Result = new ObjectResult(new ApiResponseModel<object>()
                {
                    Success = false,
                    StatusCode = code,
                    ErrorMessage = ex.InnerException?.Message ?? LangResources.DefaultErrorMessage,
                    Errors = ex.FieldErrors.Select(e => new FieldErrorModel { Field = e.Field, Message = e.Message }).ToArray()
                });

                base.OnException(context);
            }
        }
    }
}
