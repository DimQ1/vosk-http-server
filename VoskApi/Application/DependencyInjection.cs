using System.IO;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Runtime.InteropServices;
using VoskApi.Application.Feature.AudioRecognizer.Services;
using VoskApi.Application.Helpers;

namespace VoskApi.Application
{
    public static class DependencyInjection
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddSingleton<IRecognizeService, RecognizeService>();
            services.AddSingleton<IModelInitialization, ModelInitialization>();

            var buildServiceProvider = services.BuildServiceProvider();
            buildServiceProvider.GetRequiredService<IModelInitialization>();

        }
    }
}
