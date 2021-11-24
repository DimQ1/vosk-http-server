using System.IO;
using VoskApi.Application.Feature.AudioRecognizer.Models;

namespace VoskApi.Application.Feature.AudioRecognizer.Services;

public interface ITextRecognizeService
{
    TextRecognized Recognize(Stream stream);
}