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
}
