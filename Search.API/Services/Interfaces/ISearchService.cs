using Search.API.Models;

namespace Search.API.Services.Interfaces
{
    public interface ISearchService
    {
        Task<SearchResponse> Search(string query, CancellationToken ct);

    }
}
