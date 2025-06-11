using EmisTracking.Services.WebApi.Services;
using EmisTracking.WebApi.Models.Models;
using EmisTracking.WebApi.Models.ViewModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NBomber.CSharp;
using System.Net.Http.Headers;
using System.Net.WebSockets;

namespace EmisTracking.LoadTesting
{
    public class PollutantLoadTestingApiService : BaseEntityApiService<PollutantViewModel>,
        ILoadTestingHttpClient<PollutantViewModel>
    {
        protected override string ControllerPath => "pollutants";

        public PollutantLoadTestingApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient(Services.WebApi.Constants.HttpClientName);
        }

        public Task<ApiResponseModel<List<PollutantViewModel>>> GetAllAsync(string token, bool loadDependencies = false)
        {
            var path = $"{ControllerPath}";

            if (loadDependencies)
                path += "?loadDependencies=true";

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue($"Bearer", token);

            return SendRequestAsync<List<PollutantViewModel>>(HttpMethod.Get, path);
        }
    }

    public class Program
    {
        public static async Task Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", optional: false)
                        .Build();

            var apiSettings = configuration.GetSection("ApiSettings");
            var apiUrl = apiSettings["UrlAddress"];
            var email = apiSettings["Email"];
            var password = apiSettings["Password"];

            var host = Host.CreateDefaultBuilder(args)
                .ConfigureServices((context, services) =>
                {
                    services.AddHttpClient(Services.WebApi.Constants.HttpClientName, client =>
                    {
                        client.BaseAddress = new Uri(apiUrl);
                    });

                    services.AddTransient<AuthApiService>();
                    services.AddTransient<ILoadTestingHttpClient<PollutantViewModel>, PollutantLoadTestingApiService>();
                })
                .Build();

            var authApiService = host.Services.GetRequiredService<AuthApiService>();
            var pollutantApiService = host.Services.GetRequiredService<ILoadTestingHttpClient<PollutantViewModel>>();

            var authData = new LoginModel { Email = email, Password = password };

            var signInResult = await authApiService.PostSignIn(authData);

            var scenario = Scenario.Create("EmisTracking Load Tests", async context =>
            {
                return await RunTestCaseAsync(signInResult.Data, pollutantApiService);
            });

            Console.WriteLine("Please, press enter to start load testing");
            Console.ReadLine();

            var loadTest = NBomberRunner
                .RegisterScenarios(scenario
                    .WithWarmUpDuration(TimeSpan.FromSeconds(10))
                    .WithLoadSimulations(
                        Simulation.Inject(
                            rate: 50,
                            interval: TimeSpan.FromSeconds(0.5),
                            during: TimeSpan.FromSeconds(30)
            )
            ))
            .Run();

            Console.WriteLine("Done. Press enter to exit");
            Console.ReadLine();
        }

        private static async Task<NBomber.Contracts.IResponse> RunTestCaseAsync(string bearerToken, ILoadTestingHttpClient<PollutantViewModel> service, CancellationToken cancellationToken = default)
        {
            try
            {
                var getAllResponse = await service.GetAllAsync(bearerToken, true);

                if (getAllResponse == null)
                {
                    return Response.Fail(message: "Null response is returned.");
                }
                else if (getAllResponse.Success && getAllResponse.Data.Count > 0)
                {
                    return Response.Ok();
                }
                else
                {
                    return Response.Fail(message: "Nothing is returned. Please, verify the database has values");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return Response.Fail(message: $"Exception: {ex.Message}");
            }
        }
    }
}
