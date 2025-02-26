using Koneshgar.Domain.Dtos.Tasks;
using Task = Koneshgar.Domain.Models.Tasks.TaskEntity;

namespace Koneshgar.Domain.Interfaces
{
    public interface ITaskRepository : IGenericRepository<Task>
    {
        Task<Task> GetTaskWithUserTasksByIdAsync(int id);
        Task<TaskDetailDTO> GetTaskDetailByIdAsync(int id);
        Task<IEnumerable<TaskDTO>> GetTasksByUserIdAsync(string userid);
    }
}
