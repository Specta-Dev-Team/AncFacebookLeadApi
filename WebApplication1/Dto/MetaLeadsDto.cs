using System.Text.Json.Serialization;

namespace WebApplication1.Dto
{
    public class MetaLeadsDto
    {
        public string created_time { get; set; }
        public string id { get; set; }

        [JsonPropertyName("field_data")]
        public FieldData[] fieldData { get; set; }

        
    }
}
