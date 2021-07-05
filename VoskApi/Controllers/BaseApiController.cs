using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace VoskApi.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public abstract class BaseApiController : ControllerBase
    {
        private IMediator _mediator;

        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService(typeof(IMediator)) as IMediator;
    }
}
