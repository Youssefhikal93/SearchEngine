namespace Search.API.Models
{
    public class ProviderResult
    {
        public string ProviderName { get; set; }
        public string SearchTerm { get; set; } = string.Empty;
        public long TotalHits { get; set; }
        public bool IsSuccess { get; set; }
        public string? ErrorMessage { get; set; }

    }
}