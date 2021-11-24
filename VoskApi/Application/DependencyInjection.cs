using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using VoskApi.Application.Feature.AudioRecognizer.Helpers;
using VoskApi.Application.Feature.AudioRecognizer.Services;

namespace VoskApi.Application;

public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddSingleton<IRecognizeResultHelper, RecognizeResultHelper>();
        services.AddSingleton<ITextRecognizeService, TextRecognizeService>();
        services.AddSingleton<IWavUtil, WavUtil>();

        //initialization model before start api
        var initClass = ModelInitialization.SpeakerModel;
    }
}