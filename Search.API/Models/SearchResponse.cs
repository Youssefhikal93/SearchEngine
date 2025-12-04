using System.Text.Json.Serialization;

namespace Search.API.Models
{
   
    public class SearchResponse
    {
        public string Query { get; set; }
        public List<string> SearchTerm { get; set; }
        public List<ProviderResult> Results { get; set; }
    }

}
