#pragma warning disable 8618

using System.Text.Json.Serialization;

namespace HuaweiCloud.API.SDK.Models
{
    public class OcrTableRequest
    {
        [JsonPropertyName("image")]
        [JsonIgnore]
        public string Image { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("return_text_location")]
        public bool ReturnTextLocation { get; set; } = false;

        [JsonPropertyName("return_confidence")]
        public bool ReturnConfidence { get; set; } = false;

        [JsonPropertyName("return_excel")] 
        public bool ReturnExcel { get; set; } = false;
    }
}