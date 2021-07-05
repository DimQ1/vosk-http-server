using System;
using System.IO;
using System.Runtime.InteropServices;
using Vosk;
using VoskApi.Application.Helpers;

namespace VoskApi.Application.Feature.AudioRecognizer.Services
{
    public class ModelInitialization : IModelInitialization
    {
        public ModelInitialization()
        {
            Model = new Model(Path.Join(AppDomain.CurrentDomain.BaseDirectory, "model"));
        }

        public Model Model { get; }
    }
}
