using EmisTracking.WebApi.Models.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq;
using System.Net;

namespace EmisTracking.WebApi.Controllers
{
    public abstract class ErrorHandlingController : ControllerBase
    {
        protected BadRequestObjectResult CreateBadRequestResponse(ModelStateDictionary modelStateDictionary)
        {
            return BadRequest(
                new ApiResponseModel<object>
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Errors = modelStateDictionary.Select(x => new FieldErrorModel
                    {
                        Field = x.Key,
                        Message = string.Join("; ", x.Value?.Errors),
                    }).ToArray()
                }
            );
        }
    }
}
