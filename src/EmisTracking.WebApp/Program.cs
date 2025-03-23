using EmisTracking.Services.WebApi.Handlers;
using EmisTracking.Services.WebApi.Services;
using EmisTracking.WebApp.JwtAuth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace EmisTracking.WebApp
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var configuration = builder.Configuration;
            var apiUrlAddress = configuration.GetValue<string>("ApiUrlAddress");
            if (string.IsNullOrEmpty(apiUrlAddress))
            {
                Console.WriteLine("No API URL was provided. Stop.");
                return;
            }

            builder.Services.AddHttpContextAccessor();
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddScheme<AuthenticationSchemeOptions, JwtAuthenticationHandler>(
                    JwtBearerDefaults.AuthenticationScheme, null);

            builder.Services.AddTransient<IAuthApiService, AuthApiService>();

            builder.Services.AddTransient<JwtBearerTokenHandler>();

            builder.Services.AddHttpClient(Services.WebApi.Constants.HttpClientName, client =>
            {
                client.BaseAddress = new Uri(apiUrlAddress);
            })
            .AddHttpMessageHandler<JwtBearerTokenHandler>();

            builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
