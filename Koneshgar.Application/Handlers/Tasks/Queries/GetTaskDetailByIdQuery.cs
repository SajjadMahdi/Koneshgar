using Koneshgar.Application.Exceptions;
using Koneshgar.Application.Utilities.Responses.Abstract;
using Koneshgar.Application.Utilities.Responses.Concrete;
using Koneshgar.Application.Utilities.Statics;
using Koneshgar.Domain.Dtos.Tasks;
using Koneshgar.Domain.Interfaces;
using MediatR;

namespace Koneshgar.Application.Handlers.Tasks.Queries
{
    public class GetTaskDetailByIdQuery:IRequest<IResponse>
    {
        public int Id { get; set; }

        public GetTaskDetailByIdQuery(int id)
        {
            Id = id;
        }
        public class GetTaskDetailByIdQueryHandler : IRequestHandler<GetTaskDetailByIdQuery, IResponse>
        {
            private ITaskRepository _taskRepository;
            public GetTaskDetailByIdQueryHandler(ITaskRepository taskRepository)
            {
                _taskRepository = taskRepository;
            }
            
            public async Task<IResponse> Handle(GetTaskDetailByIdQuery request, CancellationToken cancellationToken)
            {
                var taskdetail = await _taskRepository.GetTaskDetailByIdAsync(request.Id);
                if (taskdetail == null)
                {
                    throw new ApiException(404, Messages.NotFound);
                }
                return new DataResponse<TaskDetailDTO>(taskdetail, 200);
            }
        }
    }
}
