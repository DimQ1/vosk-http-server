using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
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
            return Ok(await Mediator.Send(new TextRecognizeCommand() { AudioStream = file.OpenReadStream() }));
        }
    }
}
