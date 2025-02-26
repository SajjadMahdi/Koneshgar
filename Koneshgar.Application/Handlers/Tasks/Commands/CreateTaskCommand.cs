using AutoMapper;
using Koneshgar.Application.Handlers.Tasks.Validations;
using Core.Aspects.Autofac.Validation;
using Koneshgar.Application.Utilities.Responses.Abstract;
using Koneshgar.Application.Utilities.Responses.Concrete;
using Koneshgar.Application.Utilities.Statics;
using Koneshgar.Domain.Interfaces;
using Koneshgar.Domain.Models.Users;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Task = Koneshgar.Domain.Models.Tasks.TaskEntity;
using Koneshgar.Domain.Models.Tasks;
using System;

namespace Koneshgar.Application.Handlers.Tasks.Commands
{
    public class CreateTaskCommand : IRequest<IResponse>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string FromDate { get; set; }
        public string ToDate{ get; set; }
        public string StringDeadline { get; set; }
        public List<int> TasksDays{ get; set; }
        public string[] UserIds { get; set; }

        public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, IResponse>
        {
            private ITaskRepository _taskRepository;
            private IUnitOfWork _unitOfWork;
            private IHttpContextAccessor _httpContextAccessor;
            private IMapper _mapper;

            public CreateTaskCommandHandler(ITaskRepository taskRepository, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, IMapper mapper)
            {
                _taskRepository = taskRepository;
                _unitOfWork = unitOfWork;
                _httpContextAccessor = httpContextAccessor;
                _mapper = mapper;
            }

            [ValidationAspect(typeof(CreateTaskValidator))]
            public async Task<IResponse> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
            {
                var userid = _httpContextAccessor?.HttpContext?.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
                var task = _mapper.Map<Task>(request);
                task.CreatorId = userid;
                task.TaskStatusId = 1;
                task.FromTime = TimeOnly.ParseExact(request.FromDate, "HH:mm", null);
                task.ToTime = TimeOnly.ParseExact(request.ToDate, "HH:mm", null);
                task.Deadline = DateTime.ParseExact(request.StringDeadline, "dd-MM-yyyy HH:mm", null).ToUniversalTime();
                task.TasksDays = request.TasksDays.Select(i => (TaskDays)Enum.ToObject(typeof(TaskDays), i)).ToList();
                task.UserTasks = request.UserIds.Select(x => new UserTask() { UserId = x, TaskId = task.Id }).ToList();
                await _taskRepository.AddAsync(task);
                await _unitOfWork.SaveChangesAsync();
                return new SuccessResponse(200, Messages.AddedSuccesfully);
            }
        }
    }


}
