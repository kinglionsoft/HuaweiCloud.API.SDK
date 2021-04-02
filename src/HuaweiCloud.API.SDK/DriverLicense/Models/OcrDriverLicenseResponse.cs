using System;
using System.Text.Json.Serialization;
#pragma warning disable 8618

namespace HuaweiCloud.API.SDK.Models
{
    public class OcrDriverLicenseResponse
    {
        [JsonPropertyName("number")]
        public string Number { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("sex")]
        public string Sex { get; set; }

        [JsonPropertyName("nationality")]
        public string Nationality { get; set; }

        [JsonPropertyName("address")]
        public string Address { get; set; }

        [JsonPropertyName("birth")]
        public DateTime Birth { get; set; }

        [JsonPropertyName("issue_date")]
        public DateTime IssueDate { get; set; }

        [JsonPropertyName("class")]
        public string Class { get; set; }

        [JsonPropertyName("valid_from")]
        public DateTime ValidFrom { get; set; }

        [JsonPropertyName("valid_to")]
        public DateTime ValidTo { get; set; }

        [JsonPropertyName("issuing_authority")]
        public string IssuingAuthority { get; set; }

        // 以下为副证

        [JsonPropertyName("file_number")]
        public string FileNumber { get; set; }

        [JsonPropertyName("record")]
        public string Record { get; set; }
    }
}