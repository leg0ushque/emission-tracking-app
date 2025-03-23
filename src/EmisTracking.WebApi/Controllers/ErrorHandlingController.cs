using Microsoft.AspNetCore.Mvc;
using EmisTracking.WebApi.Models.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Net;
using System.Linq;

namespace EmisTracking.WebApi.Controllers
{
    public abstract class ErrorHandlingController : ControllerBase
    {
        protected BadRequestObjectResult CreateBadRequestResponse(ModelStateDictionary modelStateDictionary)
        {
            return BadRequest(
                new ErrorResponseModel
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
