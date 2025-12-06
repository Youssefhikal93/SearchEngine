using Microsoft.AspNetCore.Mvc;
using Search.API.Models;
using Search.API.Services.Interfaces;


namespace Search.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SearchController : Controller
    {
    
        private readonly ISearchService _searchService;

        public SearchController(ISearchService searchService)
        {
        
            _searchService = searchService;
        }
        [HttpPost]
        public async Task<ActionResult<ProviderResult>> Search([FromBody] SearchRequest request,CancellationToken ct)
        {
            if (string.IsNullOrWhiteSpace(request?.Query))
                return BadRequest("Query cannot be empty");

            var response = await _searchService.Search(request.Query, ct);

            return Ok(response);
        }
        


    }

}

