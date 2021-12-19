using System.IO;
using System.Threading.Tasks;
using FFMpegCore;
using FFMpegCore.Enums;
using FFMpegCore.Pipes;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;

namespace VoskApi.Application.Feature.AudioRecognizer.Helpers
{
    public class WavUtil : IWavUtil
    {
        public Stream ConvertToWavFormatForRecognize(Stream stream)
        {
            var outStream = new MemoryStream();
            var waveSource = new WaveFileReader(stream);
            var resampler = new WdlResamplingSampleProvider(waveSource.ToSampleProvider(), 16000);
            var monoSource = resampler.ToMono(1f, 1f).ToWaveProvider16();
            WaveFileWriter.WriteWavFileToStream(outStream, monoSource);

            return outStream;
        }

        public async Task<Stream> ConvertToWavFormaForRocognizeFFMpeg(Stream inputStream)
        {
            //https://github.com/rosenbjerg/FFMpegCore/issues/112
            var outputStream = new MemoryStream();
            await FFMpegArguments
                .FromPipeInput(new StreamPipeSource(inputStream))
                .OutputToPipe(new StreamPipeSink(outputStream), options => options
                .ForceFormat("s16le -ar 16k -ac 1 ")
                .WithAudioSamplingRate(16000)
                .CopyChannel(Channel.Audio)
                .WithAudioBitrate(16)
                )
                .ProcessAsynchronously();

            return outputStream;
        }
    }
}
