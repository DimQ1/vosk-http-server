using Vosk;

namespace VoskApi.Application.Feature.AudioRecognizer.Services
{
    public interface IModelInitialization
    {
        Model Model { get; }
    }
}