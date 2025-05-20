using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace EmisTracking.WebApp.Middleware
{
    public class CustomUnauthorizedMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;

        public async Task Invoke(HttpContext context)
        {
            await _next(context);

            if (context.Response.StatusCode == StatusCodes.Status401Unauthorized)
            {
                context.Response.Redirect("/Auth/login");
            }
        }
    }
}
