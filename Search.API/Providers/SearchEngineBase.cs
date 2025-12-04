using Search.API.Providers.interfaces;

namespace Search.API.Providers
{
    public abstract class SearchEngineBase : ISearchEngine
    {
        protected readonly HttpClient _httpClient;

        public abstract string EngineName { get; }
       
        protected SearchEngineBase(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<long> HitsCount(List<string> searchWords, CancellationToken ct = default)
        {
            long totalHits = 0;

            foreach (var term in searchWords)
            {
                var encoded = Uri.EscapeDataString(term);

                try
                {
                    totalHits += await GetHitsForSingleTerm(encoded, ct);
                }
                catch (Exception ex)
                {
                    throw new Exception($"{EngineName} search failed for term '{term}': {ex.Message}");
                }
            }

            return totalHits;
        }

      
        protected abstract Task<long> GetHitsForSingleTerm(string encodedTerm, CancellationToken ct);


    }
}
