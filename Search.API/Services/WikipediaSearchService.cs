using Search.API.Models;
using Search.API.Services.Interfaces;
using System.Text.Json;

namespace Search.API.Services
{

    public class WikipediaSearchService : ISearchEngine
    {
        private readonly HttpClient _httpClient;

        public string EngineName => "Wikipedia";

        public WikipediaSearchService(HttpClient httpClient)
        {
            _httpClient = httpClient;

      
            if (!_httpClient.DefaultRequestHeaders.Contains("User-Agent"))
            {
                _httpClient.DefaultRequestHeaders.Add("User-Agent",
                    "SearchEngine/1.0 (Coding Exercise)");
            }
        }

        public async Task<long> HitsCount(List<string> searchWords, CancellationToken ct = default)
        {
            long totalHits = 0;

            foreach (var term in searchWords)
            {
                var encoded = Uri.EscapeDataString(term);
                var url = $"https://en.wikipedia.org/w/api.php?action=query&format=json&list=search&utf8=1&formatversion=2&srsearch={encoded}";
            
;

                try
                {
                    var response = await _httpClient.GetAsync(url, ct);
                    response.EnsureSuccessStatusCode();

                    var content = await response.Content.ReadAsStringAsync(ct);
                    var result = JsonSerializer.Deserialize<WikipediaSearchResponse>(content);

                    
                    if (result?.Query?.SearchInfo?.TotalHits != null)
                    {
                        totalHits += result.Query.SearchInfo.TotalHits.Value;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Wikipedia search failed for term '{term}': {ex.Message}");
                }
            }

            return totalHits;
        }

    }
}
