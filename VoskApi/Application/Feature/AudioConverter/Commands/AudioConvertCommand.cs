using MediatR;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using VoskApi.Application.Feature.AudioRecognizer.Helpers;
using VoskApi.Application.Feature.AudioRecognizer.Services;

namespace VoskApi.Application.Feature.AudioConverter.Commands
{
    public class AudioConvertCommand: IRequest<Stream>
    {
        public Stream AudioStream { get; set; }

        public string FileName { get; set; }

        public class AudioConvertCommandHandler : IRequestHandler<AudioConvertCommand, Stream>
        {
            private readonly IAudioConvertService _audioConvertService;

            public AudioConvertCommandHandler(IAudioConvertService audioConvertService)
            {
                _audioConvertService = audioConvertService;
            }

            public async Task<Stream> Handle(AudioConvertCommand request, CancellationToken cancellationToken)
            {
               return await Task.Run(()=> _audioConvertService.ConvertToWavStreamForRecognize(request.AudioStream, request.FileName), cancellationToken);
            }
        }

    }
}
