#pragma warning disable 8618

using System.Text.Json.Serialization;

namespace HuaweiCloud.API.SDK.Models
{
    public class OcrVehicleLicenseRequest
    {
        [JsonPropertyName("image")]
        public string Image { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("side")] 
        public string Side { get; private set; } = "front";

        [JsonPropertyName("return_issuing_authority")] 
        public bool ReturnIssuingAuthority { get; set; } = false;

        [JsonIgnore]
        public bool Front
        {
            get => "front" == Side;
            set => Side = value ? "front" : "back";
        }
    }
}