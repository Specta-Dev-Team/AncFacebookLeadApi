using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Text.Json.Serialization;

namespace WebApplication1.Dto
{
    public class Entry
    {
        [JsonPropertyName("id")]
        public string id { get; set; }
        
        [JsonPropertyName("time")]
        public int time { get; set; }

        [JsonPropertyName("changes")]
        public Changes[] changes { get; set; } = default!;


        
     



    }
}
