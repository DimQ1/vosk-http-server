using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using VoskApi.Application.Feature.AudioRecognizer.Services;

namespace VoskApi.Application
{
    public static class DependencyInjection
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddSingleton<IRecognizeService, RecognizeService>();
            services.AddSingleton<IModelInitialization, ModelInitialization>();
            services.AddSingleton<IWavUtil, WavUtil>();

            var buildServiceProvider = services.BuildServiceProvider();
            buildServiceProvider.GetRequiredService<IRecognizeService>();

        }
    }
}
