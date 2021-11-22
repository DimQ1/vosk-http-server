using System.IO;

namespace VoskApi.Application.Feature.AudioRecognizer.Services
{
    public interface IWavUtil
    {
        Stream ConvertToWavFormatForRecognize(Stream stream);
    }
}