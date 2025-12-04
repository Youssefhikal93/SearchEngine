using Microsoft.Extensions.Caching.Memory;
using Search.API.Models;
using Search.API.Providers.interfaces;
using Search.API.Services.Interfaces;

namespace Search.API.Services
{
    public class SearchService : ISearchService
    {
        private readonly IEnumerable<ISearchEngine> _engines;
        private readonly IMemoryCache _cache;
        private const int _cacheTime = 10;

        public SearchService(IEnumerable<ISearchEngine> engines, IMemoryCache cache)
        {
            _engines = engines;
            _cache = cache;
        }

        public async Task<SearchResponse> Search(string query, CancellationToken ct)
        {
            var (cacheKey, terms) = NormalizeQuery(query);

            if (_cache.TryGetValue(cacheKey, out SearchResponse cached))
                return cached;

            var tasks = _engines.Select(e => ExecuteEngine(e, terms, ct));
            var results = await Task.WhenAll(tasks);

            var response = new SearchResponse
            {
                Query = query,
                SearchTerm = terms,
                Results = results.ToList()
            };

            _cache.Set(cacheKey, response, TimeSpan.FromMinutes(_cacheTime));

            return response;
        }

        private static (string CacheKey, List<string> Terms) NormalizeQuery(string query)
        {
            var cleaned = query.ToLower().Trim();

            var terms = cleaned
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .ToList();

            return ($"search_{cleaned}", terms);
        }

        private async Task<ProviderResult> ExecuteEngine(ISearchEngine engine, List<string> terms, CancellationToken ct)
        {
            try
            {
                var hits = await engine.HitsCount(terms, ct);
                 return new ProviderResult
                {
                    ProviderName = engine.EngineName,
                    SearchTerm = string.Join(" ", terms),
                    TotalHits = hits,
                    IsSuccess = true,
                    ErrorMessage = null
                };
            }
            catch (Exception ex)
            {
                return new ProviderResult
                {
                    ProviderName = engine.EngineName,
                    SearchTerm = string.Join(" ", terms),
                    TotalHits = 0,
                    IsSuccess = false,
                    ErrorMessage = ex.Message
                };
            }
        }
    }
}
