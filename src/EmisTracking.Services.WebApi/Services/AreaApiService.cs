using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EmisTracking.Services.WebApi.Services
{
    public class AreaApiService(IHttpClientFactory httpClientFactory)
    {
        private readonly HttpClient _httpClient = httpClientFactory.CreateClient(Constants.HttpClientName);

        public async Task<string> GetGroupsAsync()
        {
            var response = await _httpClient.GetAsync("areas");

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
    }
}
