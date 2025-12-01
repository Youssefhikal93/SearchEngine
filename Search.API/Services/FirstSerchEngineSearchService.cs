using Search.API.Services.Interfaces;
using System.Text.Json;

namespace Search.API.Services
{
    public class FirstSerchEngineSearchService : ISearchEngine
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        public string EngineName => "FirstSerchEngine";

        public FirstSerchEngineSearchService(HttpClient httpClient,
        IConfiguration config)
        {
            _httpClient = httpClient;
            _apiKey = config["FirstSerchEngine:ApiKey"] ?? "FirstSerchEngine ApiKey is missing";
        }
        public async Task<long> HitsCount(string term, CancellationToken ct = default)
        {
            try
            {
                throw new NotImplementedException(); 
            }
            catch (Exception ex)
            {
                throw new NotImplementedException();
            }
        }
    }
}
