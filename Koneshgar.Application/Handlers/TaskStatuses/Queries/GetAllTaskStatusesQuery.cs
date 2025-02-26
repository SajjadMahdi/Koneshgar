using AutoMapper;
using Koneshgar.Application.Exceptions;
using Koneshgar.Application.Utilities.Responses.Abstract;
using Koneshgar.Application.Utilities.Responses.Concrete;
using Koneshgar.Application.Utilities.Statics;
using Koneshgar.Domain.Dtos.Tasks;
using Koneshgar.Domain.Interfaces;
using MediatR;

namespace Koneshgar.Application.Handlers.TaskStatuses.Queries
{
    public class GetAllTaskStatusesQuery : IRequest<IResponse>
    {
        public class GetAllTaskStatusesQueryHandler : IRequestHandler<GetAllTaskStatusesQuery, IResponse>
        {
            private ITaskStatusRepository _taskStatusRepository;
            private IMapper _mapper;

            public GetAllTaskStatusesQueryHandler(ITaskStatusRepository taskStatusRepository, IMapper mapper)
            {
                _taskStatusRepository = taskStatusRepository;
                _mapper = mapper;
            }
            
            public async Task<IResponse> Handle(GetAllTaskStatusesQuery request, CancellationToken cancellationToken)
            {
                var taskStatuses = await _taskStatusRepository.GetAllAsync();
                var mappedtaskStatuses = _mapper.Map<IEnumerable<TaskStatusDTO>>(taskStatuses);
                return new DataResponse<IEnumerable<TaskStatusDTO>>(mappedtaskStatuses,200);
            }
        }
    }
    
}
