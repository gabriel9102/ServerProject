using System.Text.Json.Serialization;

namespace Core.Domain.Entities
{
    public class AppTask {

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }
    }
}
