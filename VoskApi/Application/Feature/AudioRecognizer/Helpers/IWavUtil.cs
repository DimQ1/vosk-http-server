using System.IO;

namespace VoskApi.Application.Feature.AudioRecognizer.Helpers
{
    public interface IWavUtil
    {
        Stream ConvertToWavFormatForRecognize(Stream stream);
    }
}