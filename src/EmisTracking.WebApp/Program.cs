using EmisTracking.Services.WebApi.Handlers;
using EmisTracking.Services.WebApi.Services;
using EmisTracking.WebApi.Models.ViewModels;
using EmisTracking.WebApp.JwtAuth;
using EmisTracking.WebApp.Middleware;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using System;

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

            RegisterHttpServices(builder.Services);

            builder.Services.AddTransient<JwtBearerTokenHandler>();

            builder.Services.AddHttpClient(Services.WebApi.Constants.HttpClientName, client =>
            {
                client.BaseAddress = new Uri(apiUrlAddress);
            })
            .AddHttpMessageHandler<JwtBearerTokenHandler>();

            builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
            builder.Services.AddControllersWithViews();

            builder.Services.AddLogging(lb =>
            {
                lb.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Information);
                lb.AddNLog(new NLogProviderOptions
                {
                    CaptureMessageTemplates = true,
                    CaptureMessageProperties = true,
                    LoggingConfigurationSectionName = "NLog",
                });
            });

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseMiddleware<CustomUnauthorizedMiddleware>();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }

        private static void RegisterHttpServices(IServiceCollection services)
        {
            services.AddTransient<IAuthApiService, AuthApiService>();

            services.AddTransient<IBaseApiService<AreaViewModel>, AreaApiService>();

            services.AddTransient<IBaseApiService<SubdivisionViewModel>, SubdivisionApiService>();
            services.AddTransient<ISubdivisionApiService, SubdivisionApiService>();

            services.AddTransient<IBaseApiService<ModeViewModel>, ModeApiService>();
            services.AddTransient<IModeApiService, ModeApiService>();

            services.AddTransient<IBaseApiService<MethodologyViewModel>, MethodologyApiService>();

            services.AddTransient<IBaseApiService<EmissionSourceViewModel>, EmissionSourceApiService>();
            services.AddTransient<IEmissionSourceApiService, EmissionSourceApiService>();

            services.AddTransient<IBaseApiService<OperatingTimeViewModel>, OperatingTimeApiService>();
            services.AddTransient<IOperatingTimeApiService, OperatingTimeApiService>();

            services.AddTransient<IBaseApiService<PollutantViewModel>, PollutantApiService>();

            services.AddTransient<IBaseApiService<SourceSubstanceViewModel>, SourceSubstanceApiService>();
            services.AddTransient<ISourceSubstanceApiService, SourceSubstanceApiService>();

            services.AddTransient<IBaseApiService<MethodologyParameterViewModel>, MethodologyParameterApiService>();
            services.AddTransient<IMethodologyParameterApiService, MethodologyParameterApiService>();

            services.AddTransient<IBaseApiService<ConsumptionGroupViewModel>, ConsumptionGroupApiService>();
            services.AddTransient<IConsumptionGroupApiService, ConsumptionGroupApiService>();

            services.AddTransient<IBaseApiService<SpecificIndicatorViewModel>, SpecificIndicatorApiService>();
            services.AddTransient<ISpecificIndicatorApiService, SpecificIndicatorApiService>();

            services.AddTransient<IBaseApiService<ConsumptionViewModel>, ConsumptionApiService>();
            services.AddTransient<IConsumptionApiService, ConsumptionApiService>();

            services.AddTransient<IBaseApiService<ParameterValueViewModel>, ParameterValueApiService>();
            services.AddTransient<IParameterValueApiService, ParameterValueApiService>();

            services.AddTransient<IBaseApiService<GrossEmissionViewModel>, GrossEmissionApiService>();
            services.AddTransient<IGrossEmissionApiService, GrossEmissionApiService>();

            services.AddTransient<IBaseApiService<TaxRateViewModel>, TaxRateApiService>();

            services.AddTransient<IBaseApiService<TaxViewModel>, TaxApiService>();

            services.AddTransient<IUserApiService, UserApiService>();
        }
    }
}
