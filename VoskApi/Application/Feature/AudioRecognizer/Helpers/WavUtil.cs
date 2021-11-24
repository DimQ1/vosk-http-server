using System.IO;
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
    }
}
