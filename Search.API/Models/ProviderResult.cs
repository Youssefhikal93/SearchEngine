namespace Search.API.Models
{
    public class ProviderResult
    {
        public string ProviderName { get; set; }
        public string SearchTerm { get; set; } = string.Empty;
        public long TotalHits { get; set; }
        public bool IsSuccess { get; set; }
        public string? ErrorMessage { get; set; }
        public List<WordHit> WordBreakdown { get; set; } = new List<WordHit>();
    }

    public class WordHit
    {
        public string Word { get; set; }
        public long Hits { get; set; }
    }
}