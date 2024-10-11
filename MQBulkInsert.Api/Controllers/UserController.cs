using MediatR;
using Microsoft.AspNetCore.Mvc;
using MQBulkInsert.Application.Commands.User;
using MQBulkInsert.Application.Queries.User;

namespace MQBulkInsert.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost]
        [Route("Import")]
        public async Task<IActionResult> ImportAsync(IFormFile file)
        {
            var command = new ImportUserCommand { File = file };
            var trackingId = await _mediator.Send(command);
            return Ok($"File uploaded successfully. TrackingId: {trackingId}");
        }

        [HttpGet]
        [Route("CheckImportState")]
        public async Task<IActionResult> GetImportStatus(string trackingId)
        {
            var query = new ImportUserStatusQuery { TrackingId = trackingId };
            var status = await _mediator.Send(query);

            return Ok(status);
        }
    }
}