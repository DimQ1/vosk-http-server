using System.IO;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using VoskApi.Application.Feature.AudioRecognizer.Models;
using VoskApi.Application.Feature.AudioRecognizer.Services;

namespace VoskApi.Application.Feature.AudioRecognizer.Commands
{
    public class TextRecognizeCommand: IRequest<TextRecognized>
    {
        public Stream AudioStream { get; set; }

        public string FileName { get; set; }

        public class TextRecognizeCommandHandler : IRequestHandler<TextRecognizeCommand, TextRecognized>
        {
            private readonly ITextRecognizeService _recognizeService;

            public TextRecognizeCommandHandler(ITextRecognizeService recognizeService)
            {
                _recognizeService = recognizeService;
            }

            public async Task<TextRecognized> Handle(TextRecognizeCommand request, CancellationToken cancellationToken)
            {
               return await Task.Run(()=>_recognizeService.Recognize(request.AudioStream, request.FileName), cancellationToken);
            }
        }

    }
}
