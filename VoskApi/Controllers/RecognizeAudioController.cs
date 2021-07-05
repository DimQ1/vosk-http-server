using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using VoskApi.Application.Feature.AudioRecognizer.Commands;

namespace VoskApi.Controllers
{
    [ApiVersion("1.0")]
    public class RecognizeAudioController : BaseApiController
    {
       private readonly ILogger<RecognizeAudioController> _logger;

        public RecognizeAudioController(ILogger<RecognizeAudioController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> UploadToFileSystem(IFormFile file)
        {
          return  Ok(await Mediator.Send(new RecognizeStreamCommand() {AudioStream = file.OpenReadStream()}));
        }


    }
}
