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
        public async Task<IActionResult> ImportAsync([FromForm]ImportUserCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet]
        [Route("CheckImportState")]
        public async Task<IActionResult> GetImportStatus([FromQuery]ImportUserStatusQuery query) {
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}