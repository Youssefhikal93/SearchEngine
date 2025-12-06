using Search.API.Models;

namespace Search.API.Providers.interfaces
{
    public interface ISearchEngine
    {
        string EngineName { get; }
        //Task<long> HitsCount(List<string> searchWords, CancellationToken ct = default);
        Task<ProviderResult> HitsCount(List<string> searchWords, CancellationToken ct = default);
    }
}
