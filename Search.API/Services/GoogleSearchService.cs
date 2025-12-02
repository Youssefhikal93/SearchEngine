using Search.API.Models;
using Search.API.Services.Interfaces;


namespace Search.API.Services
{
    public class GoogleSearchService : ISearchEngine
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly string _engineId;

        public string EngineName => "Google";

        public GoogleSearchService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _apiKey = config["Google:ApiKey"] ?? throw new Exception("Google ApiKey is missing");
            _engineId = config["Google:SearchEngineId"] ?? throw new Exception("Google EngineId (cx) is missing");
        }

        public async Task<long> HitsCount(List<string> searchWords, CancellationToken ct = default)
        {
            long totalHits = 0;

            foreach (var term in searchWords)
            {
                var encoded = Uri.EscapeDataString(term);
                var url = $"https://www.googleapis.com/customsearch/v1?key={_apiKey}&cx={_engineId}&q={encoded}";

                try
                {
                    var response = await _httpClient.GetFromJsonAsync<GoogleSearchResponse>(url, ct);
                    var totalResultsString =
                        response?.Queries?.Request?.FirstOrDefault()?.TotalResults;

                    if (totalResultsString != null &&
                        long.TryParse(totalResultsString, out long hits))
                    {
                        totalHits += hits;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Google search failed for term '{term}': {ex.Message}");
                }
            }

            return totalHits;
        }
    }
}
