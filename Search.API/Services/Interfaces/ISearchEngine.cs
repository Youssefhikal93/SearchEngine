namespace Search.API.Services.Interfaces
{
    public interface ISearchEngine
    {
        string EngineName { get; }
        Task<long> HitsCount(string term, CancellationToken ct = default);
    }
}
