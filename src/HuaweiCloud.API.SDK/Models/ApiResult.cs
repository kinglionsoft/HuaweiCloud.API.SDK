using System.Text.Json.Serialization;

namespace HuaweiCloud.API.SDK.Models
{
    internal class ApiResult
    {
        [JsonPropertyName("error_code")]
        public string ErrorCode { get; set; }

        [JsonPropertyName("error_msg")]
        public string ErrorMsg { get; set; }

        public bool Success => string.IsNullOrEmpty(ErrorCode);
    }

    internal class ApiResult<T>: ApiResult
    {
        [JsonPropertyName("result")]
        public T Result { get; set; }
    }
}