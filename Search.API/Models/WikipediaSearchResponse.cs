using System.Text.Json.Serialization;

namespace Search.API.Models
{
    public class WikipediaSearchResponse
    {
        [JsonPropertyName("query")]
        public WikipediaQuery Query { get; set; }
    }
    public class WikipediaQuery
    {
        [JsonPropertyName("searchinfo")]
        public WikipediaSearchInfo SearchInfo { get; set; }

    }
    public class WikipediaSearchInfo
    {
        [JsonPropertyName("totalhits")]
        public long? TotalHits { get; set; }
    }

}
