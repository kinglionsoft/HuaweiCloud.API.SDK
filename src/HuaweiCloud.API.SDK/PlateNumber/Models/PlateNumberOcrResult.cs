#pragma warning disable 8618
using System.Text.Json.Serialization;

namespace HuaweiCloud.API.SDK.Models
{
    public class PlateNumberOcrResult
    {
        [JsonPropertyName("plate_color")]
        public string PlateColor { get; set; }

        [JsonPropertyName("plate_number")]
        public string PlateNumber { get; set; }
    }
}
