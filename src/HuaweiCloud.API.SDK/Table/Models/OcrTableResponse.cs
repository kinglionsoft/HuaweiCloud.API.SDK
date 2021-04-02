#pragma warning disable 8618

using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HuaweiCloud.API.SDK.Models
{
    public class OcrTableResponse
    {
        [JsonPropertyName("words_region_count")]
        public int WordsRegionCount { get; set; }

        [JsonPropertyName("words_region_list")]
        public List<WordsRegion> WordsRegionList { get; set; }

        [JsonPropertyName("excel")]
        public string? Excel { get; set; }
    }

    public class WordsBlock
    {
        [JsonPropertyName("words")]
        public string Words { get; set; }
    }

    public class WordsRegion
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("words_block_count")]
        public int WordsBlockCount { get; set; }

        [JsonPropertyName("words_block_list")]
        public List<WordsBlock> WordsBlockList { get; set; }
    }
    

}