using EmisTracking.Services.WebApi.Helpers;
using System;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using EmisTracking.WebApi.Models.Models;
using EmisTracking.Localization;

namespace EmisTracking.Services.WebApi.Services
{
    public abstract class BaseApiService
    {
        protected HttpClient _httpClient;

        public virtual async Task<ApiResponseModel<T>> SendRequestAsync<T>(HttpMethod method, string url, object content = null)
        {
            try
            {
                using var request = new HttpRequestMessage(method, url);

                if (content != null)
                {
                    var jsonContent = JsonHelper.ObjectToStringContent(content);
                    request.Content = jsonContent;
                }

                var response = await _httpClient.SendAsync(request);

                return await response.DeserializeContentAsync<ApiResponseModel<T>>();
            }
            catch (Exception ex) // FIXME?
            {
                return new ApiResponseModel<T>
                {
                    Success = false,
                    ErrorMessage = LangResources.ErrorTitle,
                    StatusCode = HttpStatusCode.InternalServerError,
                };
            }
        }
    }
}
