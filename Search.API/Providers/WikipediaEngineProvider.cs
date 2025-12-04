using Search.API.Models;
using Search.API.Services.Interfaces;
using System.Text.Json;

namespace Search.API.Providers
{

    public class WikipediaEngineProvider : SearchEngineBase
    {
        public override string EngineName => "Wikipedia";

        public WikipediaEngineProvider(HttpClient httpClient) : base(httpClient)
        {
            if (!_httpClient.DefaultRequestHeaders.Contains("User-Agent"))
            {
                _httpClient.DefaultRequestHeaders.Add("User-Agent",
                    "SearchEngine/1.0 (Coding Exercise) By YHikal");
            }
        }

        protected override async Task<long> GetHitsForSingleTerm(string encodedTerm, CancellationToken ct)
        {
            var url =$"https://en.wikipedia.org/w/api.php?action=query&format=json&list=search&utf8=1&formatversion=2&srsearch={encodedTerm}";

            var totalResults = await _httpClient.GetFromJsonAsync<WikipediaSearchResponse>(url, ct);

            return totalResults?.Query.SearchInfo.TotalHits ?? 0;
        }

    }
}
