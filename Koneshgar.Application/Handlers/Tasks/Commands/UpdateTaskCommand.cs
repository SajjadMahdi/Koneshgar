using AutoMapper;
using Koneshgar.Application.Handlers.Tasks.Validations;
using Core.Aspects.Autofac.Validation;
using Koneshgar.Application.Exceptions;
using Koneshgar.Application.Utilities.Responses.Abstract;
using Koneshgar.Application.Utilities.Responses.Concrete;
using Koneshgar.Application.Utilities.Statics;
using Koneshgar.Domain.Interfaces;
using Koneshgar.Domain.Models.Users;
using MediatR;
using Koneshgar.Domain.Models.Tasks;

namespace Koneshgar.Application.Handlers.Tasks.Commands
{
    public class UpdateTaskCommand:IRequest<IResponse>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string StringDeadline { get; set; }
        public List<int> TasksDays { get; set; }
        public int TaskStatusId { get; set; }
        public string[] UserIds { get; set; }


        public class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommand, IResponse>
        {
            private ITaskRepository _taskRepository;
            private IUnitOfWork _unitOfWork;
            private IMapper _mapper;

            public UpdateTaskCommandHandler(ITaskRepository taskRepository, IUnitOfWork unitOfWork, IMapper mapper)
            {
                _taskRepository = taskRepository;
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }


            [ValidationAspect(typeof(UpdateTaskValidator))]
            public async Task<IResponse> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
            {
                var task = await _taskRepository.GetTaskWithUserTasksByIdAsync(request.Id);
                if (task == null)
                {
                    throw new ApiException(404, Messages.NotFound);
                }
                var mappedtask = _mapper.Map(request, task);
                mappedtask.UserTasks = request.UserIds.Select(userid => new UserTask()
                {
                    TaskId = task.Id,
                    UserId = userid
                }).ToList();
                //mappedtask.Deadline = DateTime.ParseExact(request.StringDeadline, "dd-MM-yyyy HH:mm",null).ToUniversalTime();
                mappedtask.FromTime = TimeOnly.ParseExact(request.StringDeadline, "HH:mm", null);
                mappedtask.ToTime = TimeOnly.ParseExact(request.StringDeadline, "HH:mm", null);
                mappedtask.Deadline = DateTime.ParseExact(request.StringDeadline, "dd-MM-yyyy HH:mm", null).ToUniversalTime();
                mappedtask.TasksDays = request.TasksDays.Select(i => (TaskDays)Enum.ToObject(typeof(TaskDays), i)).ToList();
                _taskRepository.Update(mappedtask);
                await _unitOfWork.SaveChangesAsync();
                return new SuccessResponse(200, Messages.UpdatedSuccessfully);
            }
        }
    }
}
