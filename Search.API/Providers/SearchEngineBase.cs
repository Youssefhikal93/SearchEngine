using Search.API.Models;
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

        public async Task<ProviderResult> HitsCount(List<string> searchWords, CancellationToken ct = default)
        {
            var wordBreakdown = new List<WordHit>();
            long totalHits = 0;

            foreach (var term in searchWords)
            {
                try
                {
                    var hits = await GetHitsForSingleTerm(term, ct);
                    wordBreakdown.Add(new WordHit
                    {
                        Word = term,
                        Hits = hits
                    });

                    totalHits += hits;
                }
                catch (Exception ex)
                {
                    throw new Exception($"{EngineName} search failed for term '{term}': {ex.Message}");
                }
            }

            return new ProviderResult
            {
                ProviderName = EngineName,
                SearchTerm = string.Join(" ", searchWords),
                TotalHits = totalHits,
                IsSuccess = true,
                WordBreakdown = wordBreakdown 
            };
        }

        protected abstract Task<long> GetHitsForSingleTerm(string encodedTerm, CancellationToken ct);


    }
}
