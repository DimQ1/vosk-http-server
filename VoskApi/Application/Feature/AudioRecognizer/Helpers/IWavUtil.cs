using System.IO;
using System.Threading.Tasks;

namespace VoskApi.Application.Feature.AudioRecognizer.Helpers
{
    public interface IWavUtil
    {
        Stream ConvertToWavFormatForRecognize(Stream stream);
        Task<Stream> ConvertToWavStreamForRocognizeFfMpeg(Stream inputStream);
    }
}