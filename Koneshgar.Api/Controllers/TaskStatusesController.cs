using Koneshgar.Application.Handlers.TaskStatuses.Queries;
using Koneshgar.Application.Utilities.Responses.Abstract;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Koneshgar.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskStatusesController : ControllerBase
    {
        private IMediator _mediator;

        public TaskStatusesController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]
        public async Task<IResponse> GetAll()
        {
            return await _mediator.Send(new GetAllTaskStatusesQuery());
        }
    }
}
