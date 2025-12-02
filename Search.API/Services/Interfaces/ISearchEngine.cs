namespace Search.API.Services.Interfaces
{
    public interface ISearchEngine
    {
        string EngineName { get; }
        Task<long> HitsCount(List<string> searchWords, CancellationToken ct = default);
    }
}
