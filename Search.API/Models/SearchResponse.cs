using System.Text.Json.Serialization;

namespace Search.API.Models
{
    public class GoogleSearchResponse
    {
        [JsonPropertyName("queries")]
        public GoogleQueries Queries { get; set; }
    }

    public class GoogleQueries
    {
        [JsonPropertyName("request")]
        public List<GoogleQueryRequest> Request { get; set; }
    }

    public class GoogleQueryRequest
    {
        [JsonPropertyName("totalResults")]
        public string TotalResults { get; set; }
    }
    public class WikipediaSearchResponse
    {
        [JsonPropertyName("query")]
        public WikipediaQuery Query { get; set; }
    }

    public class WikipediaQuery
    {
        [JsonPropertyName("searchinfo")]
        public WikipediaSearchInfo SearchInfo { get; set; }

        [JsonPropertyName("search")]
        public List<WikipediaSearchResult> Search { get; set; }
    }

    public class WikipediaSearchInfo
    {
        [JsonPropertyName("totalhits")]
        public long? TotalHits { get; set; }
    }

    public class WikipediaSearchResult
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("snippet")]
        public string Snippet { get; set; }
    }
    public class SearchResponse
    {
        public string Query { get; set; }
        public List<string> SearchTerm { get; set; }
        public List<ProviderResult> Results { get; set; }
    }

}
