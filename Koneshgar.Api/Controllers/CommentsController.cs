using Koneshgar.Application.Handlers.Comments.Commands;
using Koneshgar.Application.Utilities.Responses.Abstract;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Koneshgar.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CommentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpPost("CreateComment")]
        public async Task<IResponse> CreateComment(CreateCommentCommand command)
        {
            return await _mediator.Send(command);
        }
        
        [Authorize]
        [HttpPut("UpdateComment")]
        public async Task<IResponse> UpdateCommand(UpdateCommentCommand command)
        {
            return await _mediator.Send(command);
        }

        [Authorize]
        [HttpDelete("DeleteComment/{id}")]
        public async Task<IResponse> RemoveCommand(int id)
        {
            return await _mediator.Send(new RemoveCommentCommand(id));
        }
    }
}
