using System.IO;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using VoskApi.Application.Feature.AudioRecognizer.Models;
using VoskApi.Application.Feature.AudioRecognizer.Services;

namespace VoskApi.Application.Feature.AudioRecognizer.Commands
{
    public class RecognizeStreamCommand: IRequest<RecognizedChunk>
    {
        public Stream AudioStream { get; set; }

        public class RecognizeStreamCommandHandler : IRequestHandler<RecognizeStreamCommand, RecognizedChunk>
        {
            private readonly IRecognizeService _recognizeService;

            public RecognizeStreamCommandHandler(IRecognizeService recognizeService)
            {
                _recognizeService = recognizeService;
            }

            public async Task<RecognizedChunk> Handle(RecognizeStreamCommand request, CancellationToken cancellationToken)
            {
               return await Task.Run(()=>_recognizeService.Recognize(request.AudioStream), cancellationToken);
            }
        }

    }
}
