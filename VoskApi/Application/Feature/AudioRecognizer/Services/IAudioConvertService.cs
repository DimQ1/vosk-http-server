using System.IO;

namespace VoskApi.Application.Feature.AudioRecognizer.Services
{
    public interface IAudioConvertService
    {
        Stream ConvertToWavFormatForRecognize(Stream stream);
        Stream ConvertToWavStreamForRecognizeFfMpeg(Stream inputStream);

        Stream ConvertToWavStreamForRecognize(Stream stream, string filename);
    }
}