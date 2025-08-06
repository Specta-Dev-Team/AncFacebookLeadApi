using System.Text.Json.Serialization;

namespace WebApplication1.Dto
{
    public class Changes
    {
        public string field { get; set; }

        [JsonPropertyName("value")]
        public Value value { get; set; } = default!;

    }
}
