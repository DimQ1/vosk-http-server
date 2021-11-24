using Vosk;

namespace VoskApi.Application.Feature.AudioRecognizer.Services
{
    public interface IModelInitialization
    {
        Model TextModel { get; }
        SpkModel SpeakerModel { get; }
    }
}