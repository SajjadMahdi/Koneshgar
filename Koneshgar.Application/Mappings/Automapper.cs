using AutoMapper;
using Koneshgar.Application.Handlers.Tasks.Commands;
using Koneshgar.Application.Handlers.Users.Commands;
using TaskStatus = Koneshgar.Domain.Models.Tasks.TaskStatus;
using Task = Koneshgar.Domain.Models.Tasks.TaskEntity;
using Koneshgar.Application.Handlers.Comments.Commands;
using Koneshgar.Domain.Models.Tasks;
using Koneshgar.Domain.Dtos.Tasks;
using Koneshgar.Domain.Models.Users;
using Koneshgar.Domain.Dtos.User;

namespace Koneshgar.Application.Mappings
{
    public class Automapper : Profile
    {
        public Automapper()
        {
            CreateMap<User, RegisterCommand>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<TaskStatus, TaskStatusDTO>().ReverseMap();
            CreateMap<Task, CreateTaskCommand>().ReverseMap();
            CreateMap<Task, UpdateTaskCommand>().ReverseMap();
            CreateMap<Comment, CreateCommentCommand>().ReverseMap();
            CreateMap<Comment, UpdateCommentCommand>().ReverseMap();
        }
    }
}
