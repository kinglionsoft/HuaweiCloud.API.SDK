using System.Text.Json.Serialization;
#pragma warning disable 8618

namespace HuaweiCloud.API.SDK.Models
{
    public class OcrVehicleLicenseResponse
    {
        [JsonPropertyName("number")]
        public string Number { get; set; }

        [JsonPropertyName("vehicle_type")]
        public string VehicleType { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("address")]
        public string Address { get; set; }

        [JsonPropertyName("use_character")]
        public string UseCharacter { get; set; }

        [JsonPropertyName("model")]
        public string Model { get; set; }

        [JsonPropertyName("vin")]
        public string Vin { get; set; }

        [JsonPropertyName("engine_no")]
        public string EngineNo { get; set; }

        [JsonPropertyName("register_date")]
        public string RegisterDate { get; set; }

        [JsonPropertyName("issue_date")]
        public string IssueDate { get; set; }

        [JsonPropertyName("issuing_authority")]
        public string IssuingAuthority { get; set; }

        // 以下为副证

        [JsonPropertyName("file_no")]
        public string FileNo { get; set; }

        [JsonPropertyName("approved_passengers")]
        public string ApprovedPassengers { get; set; }

        [JsonPropertyName("gross_mass")]
        public string GrossMass { get; set; }

        [JsonPropertyName("unladen_mass")]
        public string UnladenMass { get; set; }

        [JsonPropertyName("approved_load")]
        public string ApprovedLoad { get; set; }

        [JsonPropertyName("dimension")]
        public string Dimension { get; set; }

        [JsonPropertyName("traction_mass")]
        public string TractionMass { get; set; }

        [JsonPropertyName("remarks")]
        public string Remarks { get; set; }

        [JsonPropertyName("inspection_record")]
        public string InspectionRecord { get; set; }

        [JsonPropertyName("code_number")]
        public string CodeNumber { get; set; }
    }


}