using Search.API.Models;
using Search.API.Services.Interfaces;


namespace Search.API.Providers
{
    public class GoogleEngineProvider : SearchEngineBase
    {
        private readonly string _apiKey;
        private readonly string _engineId;

        public override string EngineName => "Google";

        public GoogleEngineProvider(HttpClient httpClient, IConfiguration config): base(httpClient)
        {
            _apiKey = config["Google:ApiKey"] ?? throw new Exception("Google ApiKey is missing");
            _engineId = config["Google:SearchEngineId"]?? throw new Exception("Google EngineId (cx) is missing");
        }

        protected override async Task<long> GetHitsForSingleTerm(string encodedTerm, CancellationToken ct)
        {
            var url = $"https://www.googleapis.com/customsearch/v1?key={_apiKey}&cx={_engineId}&q={encodedTerm}";

            var response = await _httpClient.GetFromJsonAsync<GoogleSearchResponse>(url, ct);

            var totalResultsString =response?.Queries.Request.FirstOrDefault().TotalResults;

            if (long.TryParse(totalResultsString, out long hits))
            { 
                return hits;
            }

            return 0;
        }


    }
}
