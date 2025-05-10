using System.Net;

namespace EmisTracking.WebApi.Models.Models
{
    public class ApiResponseModel<TData>
    {
        public bool Success { get; set; }
        public TData Data { get; set; }

        public string ErrorMessage { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public FieldErrorModel[] Errors { get; set; }
    }
}
