using System.Collections.Generic;
using System.IO;
using VoskApi.Application.Feature.AudioRecognizer.Models;

namespace VoskApi.Application.Feature.AudioRecognizer.Services
{
    public interface IRecognizeService
    {
        List<RecognizedChunk> RecognizeChunks(Stream stream);
        RecognizedChunk Recognize(Stream stream);
    }
}