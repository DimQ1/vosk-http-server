using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using Vosk;
using VoskApi.Application.Feature.AudioRecognizer.Models;

namespace VoskApi.Application.Feature.AudioRecognizer.Services
{
    public class RecognizeService : IRecognizeService
    {
        private Model _model;
        private readonly IWavUtil _wavUtil;

        public RecognizeService(IModelInitialization modelInitialization, IWavUtil wavUtil)
        {
            _wavUtil = wavUtil;
            _model = modelInitialization.Model;

            Vosk.Vosk.SetLogLevel(-1);
        }

   
        public RecognizedChunk Recognize(Stream stream)
        {
            var recognizedChunks = RecognizeChunks(_wavUtil.ConvertToWavFormatForRecognize(stream));

            var results = recognizedChunks.SelectMany(ch => ch?.Result ?? new List<Result>()).ToList();
            var text = string.Join(" ", recognizedChunks.Select(ch => ch.Text).ToList());

            return new RecognizedChunk()
            {
                Result = results,
                Text = text
            };
        }

        public List<RecognizedChunk> RecognizeChunks(Stream stream)
        {
            var recognizedResults = new List<RecognizedChunk>();

            using var rec = new VoskRecognizer(_model, 16000.0f);
            rec.SetMaxAlternatives(0);
            rec.SetWords(true);

            stream.Position = 0;
            byte[] buffer = new byte[4096];
            int bytesRead;
            while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
            {
                if (rec.AcceptWaveform(buffer, bytesRead))
                {
                    var recognizedResultChunk = JsonSerializer.Deserialize<RecognizedChunk>(rec.Result());
                    recognizedResults.Add(recognizedResultChunk);
                }
            }

            recognizedResults.Add(JsonSerializer.Deserialize<RecognizedChunk>(rec.FinalResult()));

            return recognizedResults;
        }

    }
}
