using System.Text.Json.Serialization;

namespace WebApplication1.Dto
{
    public class LeadWebhookPayload
    {
        [JsonPropertyName("object")]
        public string Object { get; set; } = default!;

        [JsonPropertyName("entry")]
        public Entry[] Entry { get; set; } = default!;
    }
}
