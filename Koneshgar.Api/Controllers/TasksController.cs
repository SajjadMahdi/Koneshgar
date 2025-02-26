using Koneshgar.Application.Handlers.Tasks.Commands;
using Koneshgar.Application.Handlers.Tasks.Queries;
using Koneshgar.Application.Utilities.Responses.Abstract;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Koneshgar.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private IMediator _mediator;
        private IHttpContextAccessor _httpContextAccessor;

        public TasksController(IMediator mediator, IHttpContextAccessor httpContextAccessor)
        {
            _mediator = mediator;
            _httpContextAccessor = httpContextAccessor;
        }

        [Authorize]
        [HttpGet("GetAllTasks")]
        public async Task<IResponse> GetAllTasks()
        {
            var userid = _httpContextAccessor?.HttpContext?.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            return await _mediator.Send(new GetTasksByUserIdQuery(userid));
        }


        [Authorize]
        [HttpGet("GetTastDetailById{id}")]
        public async Task<IResponse> GetTaskDetailById(int id)
        {
            return await _mediator.Send(new GetTaskDetailByIdQuery(id));
        }
        
        [Authorize]
        [HttpPost("CreateTask")]
        public async Task<IResponse> CreateTask (CreateTaskCommand command)
        {
            return await _mediator.Send(command);
        }
        
        [Authorize]
        [HttpPut("UpdateTask")]
        public async Task<IResponse> UpdateTask(UpdateTaskCommand command)
        {
            return await _mediator.Send(command);
        }
        
        [Authorize]
        [HttpDelete("DeleteTask/{id}")]
        public async Task<IResponse> RemoveTask(int id)
        {
            return await _mediator.Send(new RemoveTaskCommand(id));
        }
    }
}
