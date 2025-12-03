using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Search.API.Models;
using Search.API.Services.Interfaces;


namespace Search.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SearchController : Controller
    {
        private readonly IEnumerable<ISearchEngine> _searchEngines;
        private readonly IMemoryCache _cache;

        public SearchController(IEnumerable<ISearchEngine> searchEngines, IMemoryCache cache)
        {
            _searchEngines = searchEngines;
            _cache = cache;

        }
        [HttpPost]
        public async Task<ActionResult<ProviderResult>> Search([FromBody] SearchRequest request,
       CancellationToken ct)
        {
            if (string.IsNullOrWhiteSpace(request?.Query) || request.Query == null)
            {
                return BadRequest( "Query cannot be empty" );
            }
            var key = $"search_{request.Query.ToLower().Trim()}";

            var terms = request.Query
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(t => t.Trim())
                .Where(t => !string.IsNullOrEmpty(t))
                .ToList();


            if (_cache.TryGetValue(key, out SearchResponse cached))
                return Ok(cached);

            var searchTasks = _searchEngines.Select(engine =>
                SearchWithEngineAsync(engine, terms, ct));

            var results = await Task.WhenAll(searchTasks);

           
            var response = new SearchResponse
            {
                Query = request.Query,
                SearchTerm = terms,
                Results = results.ToList()
            };

            _cache.Set(key, response, TimeSpan.FromMinutes(10));
            return Ok(response);
        }

        private async Task<ProviderResult> SearchWithEngineAsync(
            ISearchEngine engine,
            List<string> terms,
            CancellationToken ct)
        {
            try
            {
                
                var totalHits = await engine.HitsCount(terms, ct);

                return new ProviderResult
                {
                    ProviderName = engine.EngineName,
                    SearchTerm = string.Join(" ", terms),
                    TotalHits = totalHits,
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

