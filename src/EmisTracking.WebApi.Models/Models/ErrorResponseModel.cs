using System.Net;

namespace EmisTracking.WebApi.Models.Models
{
    public class ErrorResponseModel
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        public HttpStatusCode StatusCode { get; set; }
        public FieldErrorModel[] Errors { get; set; }
    }
}
