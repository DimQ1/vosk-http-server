using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace VoskApi.Application.Feature.AudioRecognizer.Models
{
    public class TextRecognized
    {
        [JsonPropertyName("result")]
        public List<Result> Result { get; set; }

        [JsonPropertyName("text")]
        public string Text { get; set; }
        
        [JsonPropertyName("str")]
        public string Str { get; set; }
        
        [JsonPropertyName("spk")]
        public List<float> Spk { get; set; }
        
        [JsonPropertyName("spk_frames")]
        public int SpkFrames { get; set; }

    }
}
