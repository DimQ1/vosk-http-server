using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Vosk;
using VoskApi.Application.Feature.AudioRecognizer.Models;

namespace VoskApi.Application.Feature.AudioRecognizer.Services
{
    public class TextRecognizeService : ITextRecognizeService
    {
        private readonly SpkModel _spkModel;
        private readonly Model _model;
        private readonly IAudioConvertService _audioConvertService;

        public TextRecognizeService(IAudioConvertService audioConvertService)
        {
            _audioConvertService = audioConvertService;
            _model = ModelInitialization.TextModel;
            _spkModel = ModelInitialization.SpeakerModel;

            Vosk.Vosk.GpuInit();
            Vosk.Vosk.GpuThreadInit();
            Vosk.Vosk.SetLogLevel(-1);
        }
        public TextRecognized Recognize(Stream stream, string filename)
        {
            var convertedStream = _audioConvertService.ConvertToWavStreamForRecognize(stream, filename);

            var recognizedChunks = RecognizeChunks(convertedStream);

            var results = recognizedChunks.SelectMany(ch => ch?.Result ?? new List<Result>()).ToList();
            var text = string.Join(" ", recognizedChunks.Select(ch => ch.Text).ToList());

            return new TextRecognized()
            {
                Result = results,
                Text = text,
                Str = GetSubRip(results)
            };
        }

        private string GetSubRip(List<Result> results)
        {
            var index = 1;
            return string.Join("\r\n", results.Chunk(5).Select(chank =>
                     $"{index++}\r\n{TimeSpan.FromMilliseconds(chank.First().Start * 1000):hh\\:mm\\:ss\\,fff} --> {TimeSpan.FromMilliseconds(chank.Last().End * 1000):hh\\:mm\\:ss\\,fff}\r\n{string.Join(" ", chank.Select(ch => ch.Word))}\r\n")
             );
        }

        public List<VoskTextRecognized> RecognizeChunks(Stream stream)
        {
            var recognizedResults = new List<VoskTextRecognized>();

            using var rec = new VoskRecognizer(_model, 16000.0f);
            rec.SetSpkModel(_spkModel);

            rec.SetMaxAlternatives(0);
            rec.SetWords(true);

            stream.Position = 0;
            byte[] buffer = new byte[4096];
            int bytesRead;

            while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
            {
                if (rec.AcceptWaveform(buffer, bytesRead))
                {
                    var result = rec.Result();
                    var recognizedResultChunk = JsonSerializer.Deserialize<VoskTextRecognized>(result);
                    recognizedResults.Add(recognizedResultChunk);
                }
            }

            recognizedResults.Add(JsonSerializer.Deserialize<VoskTextRecognized>(rec.FinalResult()));

            return recognizedResults;
        }

    }
}
