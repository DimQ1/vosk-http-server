using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using System.Threading.Tasks;
using VoskApi.Application.Feature.AudioConverter.Commands;
using VoskApi.Application.Feature.AudioRecognizer.Commands;

namespace VoskApi.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/{version:apiVersion}/[controller]/[action]")]
    public class RecognizeAudioController : BaseApiController
    {
        private readonly ILogger<RecognizeAudioController> _logger;

        public RecognizeAudioController(ILogger<RecognizeAudioController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        [Route("~/recognize")]
        public async Task<IActionResult> UploadFileForTheRecognizeText(IFormFile file)
        {
            return Ok(await Mediator.Send(new TextRecognizeCommand()
            {
                AudioStream = file.OpenReadStream(),
                FileName = file.FileName
            }));
        }

        [HttpPost]
        [Route("~/convert")]
        public async Task<FileContentResult> UploadFileForConvertToWav(IFormFile file)
        {
            var audioConvertCommand = new AudioConvertCommand()
            {
                AudioStream = file.OpenReadStream(),
                FileName = file.FileName,
            };

            MemoryStream stream = new MemoryStream();

            await (await Mediator.Send(audioConvertCommand)).CopyToAsync(stream);

            return new FileContentResult(stream.ToArray(), "application/octet-stream");
        }
    }
}
